using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;

namespace PhysioHub.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly PhysioHubContext _context;

        public ArticlesController (PhysioHubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.WebsiteModel = await _context.Website.OrderBy(w => w.Position).ToListAsync();

            ViewBag.ArticleMiniModel = await _context.Article.OrderByDescending(a => a.PublishedAt).Take(3).ToListAsync();


            var item = await _context.Article.FirstOrDefaultAsync (a  => a.Id == id);
            if(item == null) return NotFound();
            return View(item);
        }
    }
}
