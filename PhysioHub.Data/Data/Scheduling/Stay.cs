using PhysioHub.Data.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Data.Data.Scheduling
{
    public class Stay : BaseEntity
    {
        [Required(ErrorMessage = "Stay name is required")]
        [MaxLength(100)]
        [Display(Name = "Stay Name (e.g., Autumn 2026)")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        public required DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        public required DateTime EndDate { get; set; }

        public ICollection<StayParticipation>? StayParticipations { get; set; } = new List<StayParticipation>();
    }
}