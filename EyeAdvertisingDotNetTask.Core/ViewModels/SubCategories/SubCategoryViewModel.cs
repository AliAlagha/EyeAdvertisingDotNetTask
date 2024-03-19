using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories
{
    public class SubCategoryViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
