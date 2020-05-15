using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetSignedUrlById
{
    public class GetSignedUrlByIdQuery : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }

        public GetSignedUrlByIdQuery(string id)
        {
            Id = id;
        }
    }
}
