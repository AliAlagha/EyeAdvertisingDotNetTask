using EyeAdvertisingDotNetTask.Core.Dtos.Auth;
using EyeAdvertisingDotNetTask.Core.ViewModels.Auth;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> Login(LoginDto dto);
    }
}