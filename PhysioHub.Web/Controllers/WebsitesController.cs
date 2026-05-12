using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;

namespace PhysioHub.Web.Controllers
{
    public class WebsitesController : Controller
    {
        private readonly PhysioHubContext _context;

        public WebsitesController(PhysioHubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();


            var website = await _context.Website.FirstOrDefaultAsync(w => w.Id == id);

            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }
    }
}