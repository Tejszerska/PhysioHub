using PhysioHub.Data.Data.Base;
using PhysioHub.Data.Data.People;
using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Data.Data.Dictionaries
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