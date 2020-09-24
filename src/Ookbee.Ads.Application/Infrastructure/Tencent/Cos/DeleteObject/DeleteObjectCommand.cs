using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject
{
    public class DeleteObjectCommand : IRequest<Response<bool>>
    {
        public string Bucket { get; set; }

        public string Key { get; set; }
    }
}