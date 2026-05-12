using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Web.Controllers
{
    public class AppointmentScheduleController : Controller
    {
        private readonly PhysioHubContext _context;

        public AppointmentScheduleController(PhysioHubContext context)
        {
            _context = context;
        }

        [HttpGet]
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
                ViewBag.Error = "PESEL to short. It needs to be exactly 11 characters long";
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
    }
}
