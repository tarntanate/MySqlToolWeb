using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetByIdSignedUrl
{
    public class GetByIdSignedUrlCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }

        public GetByIdSignedUrlCommand(string id)
        {
            Id = id;
        }
    }
}
