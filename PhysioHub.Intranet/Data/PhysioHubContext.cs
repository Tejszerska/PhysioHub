using Microsoft.EntityFrameworkCore;

namespace PhysioHub.Intranet.Data
{
    public class PhysioHubContext : DbContext
    {
        public PhysioHubContext(DbContextOptions<PhysioHubContext> options)
            : base(options)
        {
        }

        public DbSet<PhysioHub.Intranet.Models.CMS.Article> Article { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.CMS.Website> Website { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Dictionaries.RehabRoom> RehabRoom { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Dictionaries.Specialization> Specialization { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Dictionaries.TreatmentCatalog> TreatmentCatalog { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.People.Patient> Patient { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.People.Therapist> Therapist { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Scheduling.AppointmentSchedule> AppointmentSchedule { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Scheduling.Stay> Stay { get; set; } = default!;
        public DbSet<PhysioHub.Intranet.Models.Scheduling.StayParticipation> StayParticipation { get; set; } = default!;
    }
}
