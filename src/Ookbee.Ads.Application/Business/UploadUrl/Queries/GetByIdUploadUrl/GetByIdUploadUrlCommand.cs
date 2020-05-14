using MediatR;
using Ookbee.Ads.Application.Business.SlotType;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrl
{
    public class GetByIdUploadUrlCommand : IRequest<HttpResult<UploadUrlDto>>
    {
        public string Id { get; }

        public GetByIdUploadUrlCommand(string id)
        {
            Id = id;
        }
    }
}
