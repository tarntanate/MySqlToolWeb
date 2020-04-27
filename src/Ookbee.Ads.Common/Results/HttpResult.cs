﻿using System;
using System.Collections.Generic;
using System.Net;

namespace Ookbee.Ads.Common.Result
{
    public class HttpResult<TValue>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public Dictionary<string, string[]> Reasons { get; set; }
        public TValue Data { get; set; }

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
