using Microsoft.EntityFrameworkCore;
using PhysioHub.Data.Data.CMS;
using PhysioHub.Data.Data.Dictionaries;
using PhysioHub.Data.Data.People;
using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Data.Data
{
    public class PhysioHubContext : DbContext
    {
        public PhysioHubContext(DbContextOptions<PhysioHubContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Article { get; set; } = default!;
        public DbSet<Website> Website { get; set; } = default!;
        public DbSet<RehabRoom> RehabRoom { get; set; } = default!;
        public DbSet<Specialization> Specialization { get; set; } = default!;
        public DbSet<TreatmentCatalog> TreatmentCatalog { get; set; } = default!;
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Therapist> Therapist { get; set; } = default!;
        public DbSet<AppointmentSchedule> AppointmentSchedule { get; set; } = default!;
        public DbSet<Stay> Stay { get; set; } = default!;
        public DbSet<StayParticipation> StayParticipation { get; set; } = default!;
    }
}
