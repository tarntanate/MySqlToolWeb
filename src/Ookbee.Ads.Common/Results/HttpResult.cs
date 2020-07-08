using System;
using System.Collections.Generic;
using System.Net;
using Ookbee.Ads.Common.Helpers;

namespace Ookbee.Ads.Common.Result
{
    public class DataLogger
    {
        public long ObjectId { get; set; }
        public object ObjectData { get; set; }
    }

    public class HttpResult<TValue>
    {
        public bool Ok { get; set; } = true;
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string StatusMessage { get; set; }
        public Dictionary<string, string[]> Reasons { get; set; }
        public TValue Data { get; set; }
        public DataLogger DataLogger { get; set; }

        public HttpResult<TValue> Success(TValue data)
        {
            return new HttpResult<TValue>()
            {
                Ok = true,
                Message = "Successfully.",
                StatusCode = HttpStatusCode.OK,
                StatusMessage = HttpStatusCode.OK.ToString(),
                Data = data,
            };
        }

        public HttpResult<TValue> Success(TValue data, long loggerId, object loggerData)
        {
            return new HttpResult<TValue>()
            {
                Ok = true,
                Message = "Successfully.",
                StatusCode = HttpStatusCode.OK,
                StatusMessage = HttpStatusCode.OK.ToString(),
                Data = data,
                DataLogger = new DataLogger
                {
                    ObjectId = loggerId,
                    ObjectData = loggerData,
                },
            };
        }

        public HttpResult<TValue> Fail(int statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            if (!Enum.IsDefined(typeof(HttpStatusCode), statusCode))
                throw new ArgumentException("Invalid HTTP response code.");
            return Fail((HttpStatusCode)statusCode, message, reasons);
        }

        public HttpResult<TValue> Fail(HttpStatusCode statusCode, string message = "An unknown error has occurred.", Dictionary<string, string[]> reasons = default)
        {
            return new HttpResult<TValue>()
            {
                Ok = false,
                Message = message,
                StatusCode = statusCode,
                StatusMessage = statusCode.ToString(),
                Reasons = reasons ?? new Dictionary<string, string[]>(),
            };
        }
    }
}
