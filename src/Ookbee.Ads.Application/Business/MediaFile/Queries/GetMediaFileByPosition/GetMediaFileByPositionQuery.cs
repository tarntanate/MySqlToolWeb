using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByPosition
{
    public class GetMediaFileByPositionQuery : IRequest<HttpResult<MediaFileDto>>
    {
        public string AdId { get; set; }

        public string Position { get; set; }

        public GetMediaFileByPositionQuery(string adId, string position)
        {
            AdId = adId;
            Position = position;
        }
    }
}
