using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById
{
    public class GetMediaFileByIdQuery : IRequest<HttpResult<MediaFileDto>>
    {
        public string Id { get; set; }

        public GetMediaFileByIdQuery(string id)
        {
            Id = id;
        }
    }
}
