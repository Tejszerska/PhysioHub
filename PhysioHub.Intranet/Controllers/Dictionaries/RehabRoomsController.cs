using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Dictionaries;

namespace PhysioHub.Intranet.Controllers.Dictionaries
{
    public class RehabRoomsController : Controller
    {
        private readonly PhysioHubContext _context;

        public RehabRoomsController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: RehabRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.RehabRoom.ToListAsync());
        }

        // GET: RehabRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehabRoom = await _context.RehabRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehabRoom == null)
            {
                return NotFound();
            }

            return View(rehabRoom);
        }

        // GET: RehabRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RehabRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNumber,Type,Id,CreatedAt,UpdatedAt,IsActive")] RehabRoom rehabRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rehabRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rehabRoom);
        }

        // GET: RehabRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehabRoom = await _context.RehabRoom.FindAsync(id);
            if (rehabRoom == null)
            {
                return NotFound();
            }
            return View(rehabRoom);
        }

        // POST: RehabRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomNumber,Type,Id,CreatedAt,UpdatedAt,IsActive")] RehabRoom rehabRoom)
        {
            if (id != rehabRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rehabRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RehabRoomExists(rehabRoom.Id))
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
            return View(rehabRoom);
        }

        // GET: RehabRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehabRoom = await _context.RehabRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehabRoom == null)
            {
                return NotFound();
            }

            return View(rehabRoom);
        }

        // POST: RehabRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rehabRoom = await _context.RehabRoom.FindAsync(id);
            if (rehabRoom != null)
            {
                _context.RehabRoom.Remove(rehabRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RehabRoomExists(int id)
        {
            return _context.RehabRoom.Any(e => e.Id == id);
        }
    }
}
