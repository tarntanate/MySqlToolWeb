using System;
using System.Collections.Generic;
using System.Net;

namespace Ookbee.Ads.Common.Result
{
    public class HttpResult<TValue>
    {
        public bool Ok { get; private set; } = true;
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
        public Dictionary<string, string[]> Reasons { get; private set; }
        public TValue Data { get; private set; }

        public HttpResult<TValue> Success(TValue data = default(TValue))
        {
            return new HttpResult<TValue>()
            {
                Ok = true,
                Message = "Successfully.",
                StatusCode = HttpStatusCode.OK,
                Data = data,
            };
        }

        public HttpResult<TValue> Fail(int statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            if (!Enum.IsDefined(typeof(HttpStatusCode), statusCode))
                throw new ArgumentException($"Invalid HTTP status code: {statusCode}");
            return Fail((HttpStatusCode)statusCode, message, reasons);
        }

        public HttpResult<TValue> Fail(HttpStatusCode statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            return new HttpResult<TValue>()
            {
                Ok = false,
                Message = message,
                StatusCode = statusCode,
                Reasons = reasons ?? new Dictionary<string, string[]>(),
            };
        }
    }
}
