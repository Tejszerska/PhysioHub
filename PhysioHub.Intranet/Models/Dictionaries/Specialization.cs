using PhysioHub.Intranet.Models.Base;
using PhysioHub.Intranet.Models.People;
using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Intranet.Models.Dictionaries
{
    public class Specialization : BaseEntity
    {
        [Required(ErrorMessage = "Specialization name is required")]
        [MaxLength(50)]
        [Display(Name = "Specialization Name")]
        public required string Name { get; set; }

        public ICollection<Therapist>? Therapists { get; set; } = new List<Therapist>();
    }
}