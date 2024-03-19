using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeAdvertisingDotNetTask.Data.Constraints
{
    public class ProductConstraints : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}