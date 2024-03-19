using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EyeAdvertisingDotNetTask.Infrastructure.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly IServiceProvider _serviceProvider;

        public ExceptionHandler(RequestDelegate next,
                                IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var response = new ApiResponseViewModel(false);
            response.Message = exception?.Message;

            switch (exception)
            {
                case EntityNotFoundException entityNotFound:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var requestBody = string.Empty;
                    var req = context.Request;
                    req.EnableBuffering();
                    if (req.Body.CanSeek)
                    {
                        req.Body.Seek(0, SeekOrigin.Begin);
                        using (var reader = new StreamReader(
                            req.Body,
                            Encoding.UTF8,
                            false,
                            8192,
                            true))
                        {
                            requestBody = await reader.ReadToEndAsync();
                        }
                        req.Body.Seek(0, SeekOrigin.Begin);
                    }

                    break;
            }

            response.ExtendedMessage = exception?.InnerException?.Message;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(response)
            );
        }
    }
}
