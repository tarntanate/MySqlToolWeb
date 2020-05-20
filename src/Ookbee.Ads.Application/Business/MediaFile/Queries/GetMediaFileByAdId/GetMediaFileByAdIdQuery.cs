using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByAdId
{
    public class GetMediaFileByAdIdQuery : IRequest<HttpResult<IEnumerable<MediaFileDto>>>
    {
        public string AdId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetMediaFileByAdIdQuery(string adId, int start, int length)
        {
            AdId = adId;
            Start = start;
            Length = length;
        }
    }
}
