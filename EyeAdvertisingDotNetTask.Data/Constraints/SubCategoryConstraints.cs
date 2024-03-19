using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeAdvertisingDotNetTask.Data.Constraints
{
    public class SubCategoryConstraints : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}