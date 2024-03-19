using EyeAdvertisingDotNetTask.Data.Constraints;
using Microsoft.EntityFrameworkCore;

namespace EyeAdvertisingDotNetTask.Data.Extentions
{
    public static class DbConstrainsExtension
    {
        public static ModelBuilder ApplyConstrains(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConstraints());
            modelBuilder.ApplyConfiguration(new ProductConstraints());
            modelBuilder.ApplyConfiguration(new SubCategoryConstraints());
            modelBuilder.ApplyConfiguration(new UserConstraints());
            
            return modelBuilder;
        }
    }
}
