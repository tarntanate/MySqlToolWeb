using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetByIdMediaFile
{
    public class GetByIdMediaFileCommand : IRequest<HttpResult<MediaFileDto>>
    {
        public string Id { get; set; }

        public GetByIdMediaFileCommand(string id)
        {
            Id = id;
        }
    }
}
