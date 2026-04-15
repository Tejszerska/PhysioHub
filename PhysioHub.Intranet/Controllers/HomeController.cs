using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Intranet.Models;
using System.Diagnostics;

namespace PhysioHub.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhysioHubContext _context;

        public HomeController(ILogger<HomeController> logger, PhysioHubContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var viewModel = new DashboardViewModel();

            // current stay 
            viewModel.CurrentStay = await _context.Stay
                .Where(s => s.StartDate <= today && s.EndDate >= today)
                .FirstOrDefaultAsync();

            // patient count
            if (viewModel.CurrentStay != null)
            {                
                viewModel.ActivePatientsCount = await _context.StayParticipation
                    .Where(sp => sp.StayId == viewModel.CurrentStay.Id)
                    .CountAsync();
            }

            // get today's treatments (.Include = JOIN)
            viewModel.TodaysTreatments = await _context.AppointmentSchedule
                .Include(a => a.Patient)
                .Include(a => a.Treatment)
                .Include(a => a.Therapist)
                .Include(a => a.Room)
                .Where(a => a.StartDateTime.Date == today)
                .OrderBy(a => a.StartDateTime)
                .ToListAsync();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
