using PhysioHub.Intranet.Models.Base;
using PhysioHub.Intranet.Models.Dictionaries;
using PhysioHub.Intranet.Models.Scheduling;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Intranet.Models.People
{
    public class Therapist : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "License number is required")]
        [MaxLength(20)]
        [Display(Name = "License Number (PWZ)")]
        public required string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [Display(Name = "Specialization")]
        public required int SpecializationId { get; set; }

        [Display(Name = "Therapist's portrait")]
        public string? PhotoLink { get; set; }

        [ForeignKey(nameof(SpecializationId))]
        public Specialization? Specialization { get; set; }

        public ICollection<AppointmentSchedule>? Appointments { get; set; } = new List<AppointmentSchedule>();
    }
}