using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data;
using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Intranet.Controllers.Scheduling
{
    public class AppointmentSchedulesController : Controller
    {
        private readonly PhysioHubContext _context;

        public AppointmentSchedulesController(PhysioHubContext context)
        {
            _context = context;
        }

        // GET: AppointmentSchedules
        public async Task<IActionResult> Index()
        {
            var physioHubContext = _context.AppointmentSchedule.Include(a => a.Patient).Include(a => a.Room).Include(a => a.StayParticipation).Include(a => a.Therapist).Include(a => a.Treatment);
            return View(await physioHubContext.Where(p => p.IsActive == true)
                                   .ToListAsync());
        }

        // GET: AppointmentSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentSchedule = await _context.AppointmentSchedule
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .Include(a => a.StayParticipation)
                .Include(a => a.Therapist)
                .Include(a => a.Treatment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentSchedule == null)
            {
                return NotFound();
            }

            return View(appointmentSchedule);
        }

        // GET: AppointmentSchedules/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullName");
            ViewData["RoomId"] = new SelectList(_context.RehabRoom, "Id", "RoomNumber");
            ViewData["StayParticipationId"] = new SelectList(_context.Set<StayParticipation>(), "Id", "Id");
            ViewData["TherapistId"] = new SelectList(_context.Therapist, "Id", "FullName");
            ViewData["TreatmentId"] = new SelectList(_context.TreatmentCatalog, "Id", "Name");
            return View();
        }

        // POST: AppointmentSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,TreatmentId,TherapistId,RoomId,StartDateTime,Status,StayParticipationId,Id,CreatedAt,UpdatedAt,IsActive")] AppointmentSchedule appointmentSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullName    ", appointmentSchedule.PatientId);
            ViewData["RoomId"] = new SelectList(_context.RehabRoom, "Id", "RoomNumber", appointmentSchedule.RoomId);
            ViewData["StayParticipationId"] = new SelectList(_context.Set<StayParticipation>(), "Id", "Id", appointmentSchedule.StayParticipationId);
            ViewData["TherapistId"] = new SelectList(_context.Therapist, "Id", "FullName", appointmentSchedule.TherapistId);
            ViewData["TreatmentId"] = new SelectList(_context.TreatmentCatalog, "Id", "Name", appointmentSchedule.TreatmentId);
            return View(appointmentSchedule);
        }

        // GET: AppointmentSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentSchedule = await _context.AppointmentSchedule.FindAsync(id);
            if (appointmentSchedule == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullName", appointmentSchedule.PatientId);
            ViewData["RoomId"] = new SelectList(_context.RehabRoom, "Id", "RoomNumber", appointmentSchedule.RoomId);
            ViewData["StayParticipationId"] = new SelectList(_context.Set<StayParticipation>(), "Id", "Id", appointmentSchedule.StayParticipationId);
            ViewData["TherapistId"] = new SelectList(_context.Therapist, "Id", "FullName", appointmentSchedule.TherapistId);
            ViewData["TreatmentId"] = new SelectList(_context.TreatmentCatalog, "Id", "Name", appointmentSchedule.TreatmentId);
            return View(appointmentSchedule);
        }

        // POST: AppointmentSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,TreatmentId,TherapistId,RoomId,StartDateTime,Status,StayParticipationId,Id,CreatedAt,UpdatedAt,IsActive")] AppointmentSchedule appointmentSchedule)
        {
            if (id != appointmentSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentScheduleExists(appointmentSchedule.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FullName", appointmentSchedule.PatientId);
            ViewData["RoomId"] = new SelectList(_context.RehabRoom, "Id", "RoomNumber", appointmentSchedule.RoomId);
            ViewData["StayParticipationId"] = new SelectList(_context.Set<StayParticipation>(), "Id", "Id", appointmentSchedule.StayParticipationId);
            ViewData["TherapistId"] = new SelectList(_context.Therapist, "Id", "FullName", appointmentSchedule.TherapistId);
            ViewData["TreatmentId"] = new SelectList(_context.TreatmentCatalog, "Id", "Name", appointmentSchedule.TreatmentId);
            return View(appointmentSchedule);
        }

        // GET: AppointmentSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentSchedule = await _context.AppointmentSchedule
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .Include(a => a.StayParticipation)
                .Include(a => a.Therapist)
                .Include(a => a.Treatment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentSchedule == null)
            {
                return NotFound();
            }

            return View(appointmentSchedule);
        }

        // POST: AppointmentSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentSchedule = await _context.AppointmentSchedule.FindAsync(id);
            if (appointmentSchedule != null)
            {
                _context.AppointmentSchedule.Remove(appointmentSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentScheduleExists(int id)
        {
            return _context.AppointmentSchedule.Any(e => e.Id == id);
        }
    }
}
