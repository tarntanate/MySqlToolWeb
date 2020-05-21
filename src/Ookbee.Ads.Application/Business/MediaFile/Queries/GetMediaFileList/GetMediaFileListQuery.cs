using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileList
{
    public class GetMediaFileListQuery : IRequest<HttpResult<IEnumerable<MediaFileDto>>>
    {
        public string AdId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetMediaFileListQuery(string adId, int start, int length)
        {
            AdId = adId;
            Start = start;
            Length = length;
        }
    }
}
