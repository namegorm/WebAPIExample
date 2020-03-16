using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Core.Application.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.API.Middleware
{
    public class CoreErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CoreErrorHandlingMiddleware> _logger;

        public CoreErrorHandlingMiddleware(RequestDelegate next, ILogger<CoreErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<CoreErrorHandlingMiddleware> logger)
        {
            var result = JsonSerializer.Serialize(new CoreResultModel(HttpStatusCode.BadRequest, "An error has occurred."));

            logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            return context.Response.WriteAsync(result);
        }
    }
}
