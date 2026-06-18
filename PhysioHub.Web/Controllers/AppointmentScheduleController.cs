using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PhysioHub.Web.Controllers
{
    public class AppointmentScheduleController : Controller
    {
        private readonly PhysioHubContext _context;

        public AppointmentScheduleController(PhysioHubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search()
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string pesel)
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            if (string.IsNullOrEmpty(pesel))
            {
                ViewBag.Error = "To check appointments PESEL must be typed";
                return View("Search");
            }

            if (pesel.Length<11)
            {
                ViewBag.Error = "PESEL too short. It needs to be exactly 11 characters long";
                return View("Search");
            }

            var appointments = await _context.AppointmentSchedule
                .Include(a => a.Treatment)
                .Include(a => a.Therapist)
                .Include(a => a.Room)
                .Where(a => a.Patient.PESEL == pesel)
                .Where(a => a.Status == Data.Enums.AppointmentStatus.Scheduled)
               .Where(a => a.StayParticipation == null ||
                         (a.StayParticipation.Stay.StartDate <= DateTime.Now && a.StayParticipation.Stay.EndDate >= DateTime.Now))
                .OrderBy(a => a.StartDateTime)
                .ToListAsync();

            if (appointments.Count == 0) 
            {
                ViewBag.Error = "No scheduled appointments found for the provided PESEL number.";
                return View("Search");
            }

            ViewBag.PatientPesel = pesel; // Pass the PESEL to the view for display


            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadSchedulePdf(string pesel)
        {
            
            if (string.IsNullOrEmpty(pesel) || pesel.Length < 11)
            {
                return BadRequest("PESEL too short. It needs to be exactly 11 characters long");
            }

            // get appointments in current Stay 
            var appointments = await _context.AppointmentSchedule
                .Include(a => a.Treatment)
                .Include(a => a.Therapist)
                .Include(a => a.Room)
                .Where(a => a.Patient.PESEL == pesel)
                .Where(a => a.Status == Data.Enums.AppointmentStatus.Scheduled)
                .Where(a => a.StayParticipation == null ||
                         (a.StayParticipation.Stay.StartDate <= DateTime.Now && a.StayParticipation.Stay.EndDate >= DateTime.Now))
                .OrderBy(a => a.StartDateTime)
                .ToListAsync();

            if (appointments.Count == 0)
            {
                return NotFound("No appointments found in the current stay for the provided PESEL number.");
            }

            // building the PDF document using QuestPDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // page settings
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    //header
                    page.Header().Column(col =>
                    {
                        col.Item().Text("PhysioHub - Appointment Schedule").SemiBold().FontSize(20).FontColor(Colors.Blue.Darken2);
                        col.Item().Text($"Appointments for the Patient (PESEL): {pesel}").FontSize(14);
                    });

                    // building table
                    page.Content().Table(table =>
                    {                        
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // date
                            columns.RelativeColumn(3); // treatment
                            columns.RelativeColumn(3); // therapist
                            columns.RelativeColumn(2); // room
                        });

                        
                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Date and Time").SemiBold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Treatment").SemiBold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Therapist").SemiBold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(2).Text("Room").SemiBold();
                        });

                        // filling the table with appointment data
                        foreach (var appt in appointments)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2)
                                 .Text(appt.StartDateTime.ToString("dd.MM.yyyy HH:mm"));

                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2)
                                 .Text(appt.Treatment.Name);

                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2)
                                 .Text(appt.Therapist.FullName);

                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(2)
                                 .Text(appt.Room.RoomNumber);
                        }
                    });

                    // footer with page numbers
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            });

            // generate PDF and return as file download
            byte[] pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Appointments_{pesel}.pdf");
        }
    }
}
