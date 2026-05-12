using Microsoft.AspNetCore.Mvc;
using PhysioHub.Data.Data;
using Microsoft.EntityFrameworkCore;


namespace PhysioHub.Web.Controllers
{
    public class StayController : Controller
    {
        private readonly PhysioHubContext _context;
        public StayController(PhysioHubContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();

            var stays = await _context.Stay.Where(s => s.EndDate >= DateTime.Now).OrderBy(s => s.StartDate).ToListAsync();

            return View(stays);
        }
    }
}
