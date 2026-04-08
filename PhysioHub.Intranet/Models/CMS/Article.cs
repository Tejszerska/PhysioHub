using PhysioHub.Intranet.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhysioHub.Intranet.Models.CMS
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
        public required int Position { get; set; } // position in the menu ?
    }
}
