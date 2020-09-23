using System;
using System.Collections.Generic;
using System.Net;

namespace Ookbee.Ads.Common.Response
{
    public class Response<T>
    {
        public bool Ok { get; private set; } = true;
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
        public Dictionary<string, string[]> Reasons { get; private set; }
        public T Data { get; private set; }

        public Response<T> Success(T data = default(T))
        {
            return new Response<T>()
            {
                Ok = true,
                Message = "Successfully.",
                StatusCode = HttpStatusCode.OK,
                Data = data,
            };
        }

        public Response<T> Fail(int statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            if (!Enum.IsDefined(typeof(HttpStatusCode), statusCode))
                throw new ArgumentException($"Invalid HTTP status code: {statusCode}");
            return Fail((HttpStatusCode)statusCode, message, reasons);
        }

        public Response<T> Fail(HttpStatusCode statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            return new Response<T>()
            {
                Ok = false,
                Message = message,
                StatusCode = statusCode,
                Reasons = reasons ?? new Dictionary<string, string[]>(),
            };
        }
    }
}
