using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.Dtos.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public int InStock { get; set; }
        public List<IFormFile> ProductImgs { get; set; }
        public int SubCategoryId { get; set; }
    }
}
