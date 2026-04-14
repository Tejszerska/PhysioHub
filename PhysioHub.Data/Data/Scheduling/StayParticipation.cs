
using PhysioHub.Data.Data.Base;
using PhysioHub.Data.Data.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Data.Data.Scheduling
{
    public class StayParticipation : BaseEntity
    {
        [Required]
        public required int PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }

        [Required]
        public required int StayId { get; set; }

        [ForeignKey(nameof(StayId))]
        public Stay? Stay { get; set; }
    }
}