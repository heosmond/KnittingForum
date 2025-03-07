using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KnittingForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData] 
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        [PersonalData]
        public string Bio { get; set; } = string.Empty;

        [PersonalData]
        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
