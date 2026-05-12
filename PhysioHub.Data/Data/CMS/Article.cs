using PhysioHub.Data.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Data.Data.CMS
{
    public class Article : BaseEntity
    {
        [Required(ErrorMessage = "Title of the link is required")]
        [MaxLength(20, ErrorMessage = "Title of the link cannot be longer than 20 characters")]
        [Display(Name = "Link Title")]
        public required string LinkTitle { get; set; }


        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title of the article cannot be longer than 50 characters")]
        public required string Title { get; set; }


        [Required(ErrorMessage = "Content is required")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Content { get; set; }


        [Required(ErrorMessage = "Position is required")]
        public required int Position { get; set; }

        [Required(ErrorMessage = "Date of publishing is required")]
        public required DateTime PublishedAt { get; set; }

        [Required(ErrorMessage = "Short description is required")]
        [MaxLength(100, ErrorMessage = "Short description cannot be longer than 100 characters")]
        public required string ShortDescription { get; set; }

    }
}
