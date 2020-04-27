using Ookbee.Ads.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ValidationException = Ookbee.Ads.Common.Exceptions.ValidationException;

namespace Ookbee.Ads.Common.AspNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode;
            if (!context.ModelState.IsValid)
            {
                statusCode = HttpStatusCode.BadRequest;
                var errors = context
                    .ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                context.Result = new JsonResult(new
                {
                    message = "The request is invalid in the current state.",
                    errors = errors.SelectMany(v => v.Value.Select(e => e)).ToList()
                });
            }
            else if (context.Exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                var errors = ((ValidationException)context.Exception).Failures;
                context.Result = new JsonResult(new
                {
                    message = context.Exception.Message,
                    errors = errors.SelectMany(v => v.Value.Select(e => e)).ToList()
                });
            }
            else if (context.Exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                context.Result = new JsonResult(new { });
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(new
                {
                    message = "Internal Server Error",
                    errors = new List<string>(),
                    exception = context.Exception
                });
            }
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }
    }
}