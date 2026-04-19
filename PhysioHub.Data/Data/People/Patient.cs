
using PhysioHub.Data.Data.Base;
using PhysioHub.Data.Data.Scheduling;
using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Data.Data.People
{
    public class Patient : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "PESEL is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "National ID must be exactly 11 characters long")]
        public required string PESEL { get; set; }

        [MaxLength(20)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public ICollection<StayParticipation>? StayParticipations { get; set; } = new List<StayParticipation>();
        public ICollection<AppointmentSchedule>? Appointments { get; set; } = new List<AppointmentSchedule>();

        public string FullName => $"{FirstName} {LastName}";
    }
}