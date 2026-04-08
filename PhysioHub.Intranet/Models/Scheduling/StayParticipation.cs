using PhysioHub.Intranet.Models.Base;
using PhysioHub.Intranet.Models.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Intranet.Models.Scheduling
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