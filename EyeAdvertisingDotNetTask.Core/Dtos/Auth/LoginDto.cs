using System.ComponentModel.DataAnnotations;

namespace EyeAdvertisingDotNetTask.Core.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}