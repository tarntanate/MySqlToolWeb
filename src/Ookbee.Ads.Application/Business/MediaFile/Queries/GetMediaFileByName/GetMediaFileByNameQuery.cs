using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQuery : IRequest<HttpResult<MediaFileDto>>
    {
        public string Name { get; set; }

        public GetMediaFileByNameQuery(string name)
        {
            Name = name;
        }
    }
}
