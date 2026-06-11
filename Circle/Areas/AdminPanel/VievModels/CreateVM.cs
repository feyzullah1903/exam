using System.ComponentModel.DataAnnotations;

namespace Circle.Areas.AdminPanel.VievModels
{
    public class CreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
 
        public string Description { get; set; }
        
    }
}
