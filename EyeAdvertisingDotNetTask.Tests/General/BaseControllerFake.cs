using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EyeAdvertisingDotNetTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseControllerFake : Controller
    {
        protected async Task<ActionResult> GetResponse(Func<Task<ApiResponseViewModel>> func)
        {
            return Ok(await func());
        }

        protected ActionResult GetResponse(Func<ApiResponseViewModel> func)
        {
            return Ok(func());
        }

    }
}