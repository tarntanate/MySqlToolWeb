using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.AspNetCore.Middlewares
{
    public class HttpExceptionMiddleware
    {
        private RequestDelegate _next { get; }
        private ILogger<HttpExceptionMiddleware> _logger { get; }

        public HttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning($"The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                _logger.LogError($"{exception}");

                var json = JsonConvert.SerializeObject(new
                {
                    apiVersion = "1.0",
                    ok = false,
                    error = new
                    {
                        message = "One or more errors occurred while processing the request.",
                        exception = exception.Message,
                        innerException = exception.InnerException,
                        reasons = new Dictionary<string, string>(),
                        source = exception.Source,
                        stackTrace = exception.StackTrace,
                        helpLink = exception.HelpLink,
                        hResult = exception.HResult,
                        data = exception.Data,
                    },
                },
                Formatting.None,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);

                return;
            }
        }
    }

    public static class HttpExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpExceptionMiddleware>();
        }
    }
}