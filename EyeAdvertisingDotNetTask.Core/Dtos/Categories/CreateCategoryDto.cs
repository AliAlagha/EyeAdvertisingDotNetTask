using System.ComponentModel.DataAnnotations;

namespace EyeAdvertisingDotNetTask.Core.Dtos.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }
    }
}
