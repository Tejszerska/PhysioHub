using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.People;

namespace PhysioHub.Intranet.Controllers.People
{
    public class TherapistsController : Controller
    {
        private readonly PhysioHubContext _context;

        public TherapistsController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: Therapists
        public async Task<IActionResult> Index()
        {
            var physioHubContext = _context.Therapist.Include(t => t.Specialization);
            return View(await physioHubContext.Where(p => p.IsActive == true)
                                   .ToListAsync());
        }

        // GET: Therapists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapist
                .Include(t => t.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (therapist == null)
            {
                return NotFound();
            }

            return View(therapist);
        }

        // GET: Therapists/Create
        public IActionResult Create()
        {
            ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name");
            return View();
        }

        // POST: Therapists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,LicenseNumber,SpecializationId,PhotoLink,Id,CreatedAt,UpdatedAt,IsActive")] Therapist therapist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(therapist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", therapist.SpecializationId);
            return View(therapist);
        }

        // GET: Therapists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapist.FindAsync(id);
            if (therapist == null)
            {
                return NotFound();
            }
            ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", therapist.SpecializationId);
            return View(therapist);
        }

        // POST: Therapists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,LicenseNumber,SpecializationId,PhotoLink,Id,CreatedAt,UpdatedAt,IsActive")] Therapist therapist)
        {
            if (id != therapist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(therapist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TherapistExists(therapist.Id))
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
            ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", therapist.SpecializationId);
            return View(therapist);
        }

        // GET: Therapists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapist
                .Include(t => t.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (therapist == null)
            {
                return NotFound();
            }

            return View(therapist);
        }

        // POST: Therapists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var therapist = await _context.Therapist.FindAsync(id);
            if (therapist != null)
            {
                _context.Therapist.Remove(therapist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TherapistExists(int id)
        {
            return _context.Therapist.Any(e => e.Id == id);
        }
    }
}
