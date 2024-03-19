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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        protected string UserId;

        protected async Task<ActionResult> GetResponse(Func<Task<ApiResponseViewModel>> func)
        {
            return Ok(await func());
        }

        protected ActionResult GetResponse(Func<ApiResponseViewModel> func)
        {
            return Ok(func());
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (User.Identity.IsAuthenticated)
            {
                UserId = User.FindFirst(Claims.UserId).Value;
            }
        }

    }
}