using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeAdvertisingDotNetTask.Data.DbEntities
{
    public class User : IdentityUser
    {
        public User()
        {
            IsDeleted = false;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedById { get; set; }
        public string? UpdatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}