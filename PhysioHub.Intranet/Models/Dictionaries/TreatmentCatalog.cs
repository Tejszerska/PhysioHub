using PhysioHub.Intranet.Models.Base;
using PhysioHub.Intranet.Models.Scheduling;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Intranet.Models.Dictionaries
{
    public class TreatmentCatalog : BaseEntity
    {
        [Required(ErrorMessage = "Treatment name is required")]
        [MaxLength(100)]
        [Display(Name = "Treatment Name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration (minutes)")]
        public required int DurationMinutes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        public ICollection<AppointmentSchedule>? Appointments { get; set; } = new List<AppointmentSchedule>();
    }
}