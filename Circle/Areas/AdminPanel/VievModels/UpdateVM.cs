using System.ComponentModel.DataAnnotations;

namespace Circle.Areas.AdminPanel.VievModels
{
    public class UpdateVM
    {
        [Required]
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Desc   { get; set; }
        [Required]
    
        public int Id { get; set; }
    }
}
