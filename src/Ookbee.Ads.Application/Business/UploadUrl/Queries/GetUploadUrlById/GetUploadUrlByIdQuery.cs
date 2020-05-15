using MediatR;
using Ookbee.Ads.Application.Business.SlotType;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById
{
    public class GetUploadUrlByIdQuery : IRequest<HttpResult<UploadUrlDto>>
    {
        public string Id { get; }

        public GetUploadUrlByIdQuery(string id)
        {
            Id = id;
        }
    }
}
