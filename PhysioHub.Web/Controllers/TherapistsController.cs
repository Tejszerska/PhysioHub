using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;

namespace PhysioHub.Web.Controllers
{
    public class TherapistsController : Controller
    {
        private readonly PhysioHubContext _context;
        public TherapistsController(PhysioHubContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();

            var therpiasts = await _context.Therapist.Include(t => t.Specialization).ToListAsync();

            return View(therpiasts);
        }
    }
}
