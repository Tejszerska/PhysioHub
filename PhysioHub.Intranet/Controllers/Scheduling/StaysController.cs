using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Intranet.Controllers.Scheduling
{
    public class StaysController : Controller
    {
        private readonly PhysioHubContext _context;

        public StaysController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: Stays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stay.ToListAsync());
        }

        // GET: Stays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // GET: Stays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,Id,CreatedAt,UpdatedAt,IsActive")] Stay stay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stay);
        }

        // GET: Stays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stay.FindAsync(id);
            if (stay == null)
            {
                return NotFound();
            }
            return View(stay);
        }

        // POST: Stays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,StartDate,EndDate,Id,CreatedAt,UpdatedAt,IsActive")] Stay stay)
        {
            if (id != stay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StayExists(stay.Id))
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
            return View(stay);
        }

        // GET: Stays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stay = await _context.Stay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // POST: Stays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stay = await _context.Stay.FindAsync(id);
            if (stay != null)
            {
                _context.Stay.Remove(stay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StayExists(int id)
        {
            return _context.Stay.Any(e => e.Id == id);
        }
    }
}
