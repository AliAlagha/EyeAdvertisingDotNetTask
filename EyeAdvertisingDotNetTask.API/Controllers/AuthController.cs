using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Auth;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EyeAdvertisingDotNetTask.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<ActionResult> Register(RegisterDto dto)
           => GetResponse(async () => new ApiResponseViewModel(await _authService.Register(dto), true, Messages.SuccessfulOperation));

        [HttpPost]
        [AllowAnonymous]
        public Task<ActionResult> Login(LoginDto dto)
           => GetResponse(async () => new ApiResponseViewModel(await _authService.Login(dto), true, Messages.SuccessfulOperation));

        [HttpPost]
        public Task<ActionResult> RefreshToken(RefreshTokenDto dto)
            => GetResponse(async () => new ApiResponseViewModel(await _authService.RefreshToken(dto), true, Messages.SuccessfulOperation));
    }
}