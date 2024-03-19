using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeAdvertisingDotNetTask.Data.Constraints
{
    public class CategoryConstraints : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}