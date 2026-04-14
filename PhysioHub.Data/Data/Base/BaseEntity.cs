using System.ComponentModel.DataAnnotations;

namespace PhysioHub.Data.Data.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}