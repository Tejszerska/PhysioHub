using PhysioHub.Data.Data.Base;
using PhysioHub.Data.Data.Scheduling;
using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Data.Data.Dictionaries
{
    public class RehabRoom : BaseEntity
    {
        [Required(ErrorMessage = "Room number or name is required")]
        [MaxLength(20)]
        [Display(Name = "Room Number/Name")]
        public required string RoomNumber { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [MaxLength(50)]
        [Display(Name = "Type (e.g., Wet, Dry, Gym)")]
        public required string Type { get; set; }

        public ICollection<AppointmentSchedule>? Appointments { get; set; } = new List<AppointmentSchedule>();
    }
}