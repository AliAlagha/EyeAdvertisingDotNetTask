using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.Categories
{
    public class CategoryViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
