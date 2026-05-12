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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries();

            foreach (var entityEntry in entries)
            {
                // set DateTime for update
                if (entityEntry.Metadata.FindProperty("UpdatedAt") != null && entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                // set DateTime for create
                if (entityEntry.State == EntityState.Added)
                {
                    if (entityEntry.Metadata.FindProperty("CreatedAt") != null)
                    {
                        entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    }

                    // set active when creating
                    if (entityEntry.Metadata.FindProperty("IsActive") != null)
                    {
                        entityEntry.Property("IsActive").CurrentValue = true;
                    }
                }

                // SOFT DELETE
                if (entityEntry.State == EntityState.Deleted && entityEntry.Metadata.FindProperty("IsActive") != null)
                {
                    // blocks removing record from database
                    entityEntry.State = EntityState.Modified;

                    // set not active
                    entityEntry.Property("IsActive").CurrentValue = false;

                    // set modified time
                    if (entityEntry.Metadata.FindProperty("UpdatedAt") != null)
                        entityEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        // saves enum as name instead of number in database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentSchedule>()
                .Property(e => e.Status)
                .HasConversion<string>(); 
        }
    }
}
