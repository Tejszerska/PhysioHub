using PhysioHub.Data.Data.Base;
using PhysioHub.Data.Data.Dictionaries;
using PhysioHub.Data.Data.People;
using PhysioHub.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Data.Data.Scheduling
{
    public class AppointmentSchedule : BaseEntity
    {
        [Required(ErrorMessage = "Patient is required")]
        [Display(Name = "Patient")]
        public required int PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }

        [Required(ErrorMessage = "Treatment is required")]
        [Display(Name = "Treatment")]
        public required int TreatmentId { get; set; }

        [ForeignKey(nameof(TreatmentId))]
        public TreatmentCatalog? Treatment { get; set; }

        [Required(ErrorMessage = "Therapist is required")]
        [Display(Name = "Therapist")]
        public required int TherapistId { get; set; }

        [ForeignKey(nameof(TherapistId))]
        public Therapist? Therapist { get; set; }

        [Required(ErrorMessage = "Room is required")]
        [Display(Name = "Rehab Room")]
        public required int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public RehabRoom? Room { get; set; }

        [Required(ErrorMessage = "Start date and time are required")]
        [Display(Name = "Start Date & Time")]
        public required DateTime StartDateTime { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [Display(Name = "Stay (Optional)")]
        public int? StayParticipationId { get; set; }

        [ForeignKey(nameof(StayParticipationId))]
        public StayParticipation? StayParticipation { get; set; }
    }
}