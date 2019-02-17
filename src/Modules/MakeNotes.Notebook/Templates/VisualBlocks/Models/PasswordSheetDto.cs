using System.ComponentModel.DataAnnotations;

namespace MakeNotes.Notebook.Templates.VisualBlocks.Models
{
    public class PasswordSheetDto
    {
        public int? Id { get; set; }
        
        [Required]
        [Display(Name = nameof(Site))]
        public string Site { get; set; }
        
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
