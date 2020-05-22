using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject
{
    public class DeleteObjectCommand : IRequest<HttpResult<bool>>
    {
        public string Bucket { get; set; }

        public string Key { get; set; }
    }
}