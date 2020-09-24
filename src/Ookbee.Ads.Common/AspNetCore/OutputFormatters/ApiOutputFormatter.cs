using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.AspNetCore.OutputFormatters
{
    public class ApiOutputFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var obj = (dynamic)context.Object;
            var objType = context.ObjectType;
            var responseBody = new object();
            var responseStatusCode = (HttpStatusCode)obj.StatusCode;

            if (IsHttpResult(objType))
            {
                if (obj.Ok)
                {
                    obj = obj?.Data;
                    objType = obj?.GetType();
                    responseBody = new
                    {
                        Data = IsEnumnerable(objType)
                            ? new { items = obj }
                            : obj
                    };
                }
                else
                {
                    var reasons = obj.Reasons as Dictionary<string, string[]>;
                    responseBody = new
                    {
                        message = "One or more errors occurred while processing the request.",
                        errors = reasons.Count > 0
                            ? obj.Reasons
                            : new string[] { obj.Message }
                    };
                }
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)responseStatusCode;
            return context.HttpContext.Response.WriteAsync(JsonHelper.Serialize(responseBody));
        }

        private bool IsEnumnerable(Type objType)
        {
            return typeof(IEnumerable<object>).IsAssignableFrom(objType);
        }

        private bool IsHttpResult(Type objType)
        {
            if (objType.IsGenericType)
                return typeof(Response<>).IsAssignableFrom(objType.GetGenericTypeDefinition());
            return false;
        }
    }
}
