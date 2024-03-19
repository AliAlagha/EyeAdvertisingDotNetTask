using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyeAdvertisingDotNetTask.Data.Constraints
{
    public class UserConstraints : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}