using EyeAdvertisingDotNetTask.Core.Dtos.Auth;
using EyeAdvertisingDotNetTask.Core.ViewModels.Auth;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);
        Task<LoginResponseViewModel> Login(LoginDto dto);
        Task<LoginResponseViewModel> RefreshToken(RefreshTokenDto dto);
    }
}