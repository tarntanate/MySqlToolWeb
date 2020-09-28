using System;
using System.Collections.Generic;
using System.Net;

namespace Ookbee.Ads.Common.Response
{
    public class Response<T>
    {
        public bool IsSuccess { get; private set; } = false;
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
        public Dictionary<string, string[]> Reasons { get; private set; }
        public T Data { get; private set; }

        public Response<T> OK(T data = default(T))
        {
            return new Response<T>()
            {
                IsSuccess = true,
                Message = "Successfully.",
                StatusCode = HttpStatusCode.OK,
                Data = data,
            };
        }

        public Response<T> NotFound(string message = "Data not found.", Dictionary<string, string[]> reasons = default)
        {
            return new Response<T>()
            {
                Message = message,
                StatusCode = HttpStatusCode.NotFound,
                Reasons = reasons ?? new Dictionary<string, string[]>(),
            };
        }

        public Response<T> Forbidden(string message = "Access denied.", Dictionary<string, string[]> reasons = default)
        {
            return new Response<T>()
            {
                Message = message,
                StatusCode = HttpStatusCode.Forbidden,
            };
        }

        public Response<T> Unauthorized(string message = "Unauthorized Access.", Dictionary<string, string[]> reasons = default)
        {
            return new Response<T>()
            {
                Message = message,
                StatusCode = HttpStatusCode.Unauthorized,
            };
        }

        public Response<T> Status(HttpStatusCode statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            return new Response<T>()
            {
                IsSuccess = Math.Round((int)statusCode / 100d, 0) * 100 == 200 ? true : false,
                Message = message,
                StatusCode = statusCode,
                Reasons = reasons ?? new Dictionary<string, string[]>(),
                Data = default(T)
            };
        }
    }
}
