using Microsoft.AspNetCore.Mvc;
using PhysioHub.Data.Data;
using PhysioHub.Web.Models;
using System.Diagnostics;

namespace PhysioHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhysioHubContext _context;

        public HomeController(PhysioHubContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.WebsiteModel=
                (
                from website in _context.Website
                orderby website.Position
                select website
                ).ToList();

            if (id == null) id = 1; 
            var item= await _context.Website.FindAsync(id);
            return View(item);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ========= for 1st laboratory ===========
        public IActionResult Offer()
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult Contact()
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
