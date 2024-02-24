using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace CMSBlogMaster_BL.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _exception;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(RequestDelegate exception, ILogger<CustomExceptionMiddleware> logger)
        {
            _exception = exception;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _exception(context);
            }
            catch (CustomException ex)
            {
                // Handle the custom exception
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {"Message: " + ex.Message + " | Stack Trace: " + ex.StackTrace.ToString()}");
                // Handle other exceptions here, if needed
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
