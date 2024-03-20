using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.Dtos.Products
{
    public class UpdateProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int InStock { get; set; }

        public List<IFormFile>? ProductImgs { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
    }
}
