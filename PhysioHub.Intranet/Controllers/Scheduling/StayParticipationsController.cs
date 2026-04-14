using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Intranet.Controllers.Scheduling
{
    public class StayParticipationsController : Controller
    {
        private readonly PhysioHubContext _context;

        public StayParticipationsController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: StayParticipations
        public async Task<IActionResult> Index()
        {
            var physioHubContext = _context.StayParticipation.Include(s => s.Patient).Include(s => s.Stay);
            return View(await physioHubContext.ToListAsync());
        }

        // GET: StayParticipations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stayParticipation = await _context.StayParticipation
                .Include(s => s.Patient)
                .Include(s => s.Stay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stayParticipation == null)
            {
                return NotFound();
            }

            return View(stayParticipation);
        }

        // GET: StayParticipations/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName");
            ViewData["StayId"] = new SelectList(_context.Stay, "Id", "Name");
            return View();
        }

        // POST: StayParticipations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,StayId,Id,CreatedAt,UpdatedAt,IsActive")] StayParticipation stayParticipation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stayParticipation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", stayParticipation.PatientId);
            ViewData["StayId"] = new SelectList(_context.Stay, "Id", "Name", stayParticipation.StayId);
            return View(stayParticipation);
        }

        // GET: StayParticipations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stayParticipation = await _context.StayParticipation.FindAsync(id);
            if (stayParticipation == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", stayParticipation.PatientId);
            ViewData["StayId"] = new SelectList(_context.Stay, "Id", "Name", stayParticipation.StayId);
            return View(stayParticipation);
        }

        // POST: StayParticipations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,StayId,Id,CreatedAt,UpdatedAt,IsActive")] StayParticipation stayParticipation)
        {
            if (id != stayParticipation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stayParticipation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StayParticipationExists(stayParticipation.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", stayParticipation.PatientId);
            ViewData["StayId"] = new SelectList(_context.Stay, "Id", "Name", stayParticipation.StayId);
            return View(stayParticipation);
        }

        // GET: StayParticipations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stayParticipation = await _context.StayParticipation
                .Include(s => s.Patient)
                .Include(s => s.Stay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stayParticipation == null)
            {
                return NotFound();
            }

            return View(stayParticipation);
        }

        // POST: StayParticipations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stayParticipation = await _context.StayParticipation.FindAsync(id);
            if (stayParticipation != null)
            {
                _context.StayParticipation.Remove(stayParticipation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StayParticipationExists(int id)
        {
            return _context.StayParticipation.Any(e => e.Id == id);
        }
    }
}
