using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Dictionaries;

namespace PhysioHub.Intranet.Controllers.Dictionaries
{
    public class TreatmentCatalogsController : Controller
    {
        private readonly PhysioHubContext _context;

        public TreatmentCatalogsController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: TreatmentCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TreatmentCatalog.Where(p => p.IsActive == true)
                                   .ToListAsync());
        }

        // GET: TreatmentCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentCatalog = await _context.TreatmentCatalog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treatmentCatalog == null)
            {
                return NotFound();
            }

            return View(treatmentCatalog);
        }

        // GET: TreatmentCatalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TreatmentCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DurationMinutes,Price,Id,CreatedAt,UpdatedAt,IsActive")] TreatmentCatalog treatmentCatalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatmentCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treatmentCatalog);
        }

        // GET: TreatmentCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentCatalog = await _context.TreatmentCatalog.FindAsync(id);
            if (treatmentCatalog == null)
            {
                return NotFound();
            }
            return View(treatmentCatalog);
        }

        // POST: TreatmentCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DurationMinutes,Price,Id,CreatedAt,UpdatedAt,IsActive")] TreatmentCatalog treatmentCatalog)
        {
            if (id != treatmentCatalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatmentCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentCatalogExists(treatmentCatalog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(treatmentCatalog);
        }

        // GET: TreatmentCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentCatalog = await _context.TreatmentCatalog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treatmentCatalog == null)
            {
                return NotFound();
            }

            return View(treatmentCatalog);
        }

        // POST: TreatmentCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatmentCatalog = await _context.TreatmentCatalog.FindAsync(id);
            if (treatmentCatalog != null)
            {
                _context.TreatmentCatalog.Remove(treatmentCatalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentCatalogExists(int id)
        {
            return _context.TreatmentCatalog.Any(e => e.Id == id);
        }
    }
}
