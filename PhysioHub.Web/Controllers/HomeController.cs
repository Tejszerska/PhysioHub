using Microsoft.AspNetCore.Mvc;
using PhysioHub.Data.Data;
using PhysioHub.Web.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();

            if (id == null) id = 1; 
            var item= await _context.Website.FindAsync(id);

            return View(item);
        }

        public async Task<IActionResult> Contact()
        {

            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
