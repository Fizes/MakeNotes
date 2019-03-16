using System.ComponentModel.DataAnnotations;
using MakeNotes.Common.Models;
using MakeNotes.Framework.Attributes;

namespace MakeNotes.Notebook.Templates.VisualBlocks.Models
{
    public class PasswordSheetDto : IVisualBlock
    {
        [Ignore]
        public int? Id { get; set; }

        [Required]
        [Display(Name = nameof(Site))]
        public string Site { get; set; }

        [Required]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        [Display(Name = nameof(Description))]
        public string Description { get; set; }
    }
}
