using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
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
            var responseValue = new object();
            var statusCode = HttpStatusCode.OK;

            if (IsHttpResult(objType))
            {
                if (obj.Ok)
                {
                    obj = obj?.Data;
                    objType = obj?.GetType();
                }
                else
                {
                    statusCode = (HttpStatusCode)obj.StatusCode;
                    if (obj.Message != null)
                    {
                        responseValue = new
                        {
                            message = obj.Message,
                            errors = obj.Reasons
                        };
                    }
                }
            }

            if (statusCode == HttpStatusCode.OK)
            {
                responseValue = new
                {
                    Data = IsEnumnerable(objType) ? new { items = obj } : obj
                };
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            return context.HttpContext.Response.WriteAsync(JsonHelper.Serialize(responseValue));
        }

        private bool IsEnumnerable(Type objType)
        {
            return typeof(IEnumerable<object>).IsAssignableFrom(objType);
        }

        private bool IsHttpResult(Type objType)
        {
            if (objType.IsGenericType)
                return typeof(HttpResult<>).IsAssignableFrom(objType.GetGenericTypeDefinition());
            else
                return false;
        }
    }
}
