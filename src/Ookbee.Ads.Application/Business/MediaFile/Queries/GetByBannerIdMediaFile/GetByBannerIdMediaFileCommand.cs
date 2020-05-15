using System.Collections.Generic;
using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetByBannerIdMediaFile
{
    public class GetByBannerIdMediaFileCommand : IRequest<HttpResult<IEnumerable<MediaFileDto>>>
    {
        public string BannerId { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public GetByBannerIdMediaFileCommand(string bannerId, int start, int length)
        {
            BannerId = bannerId;
            Start = start;
            Length = length;
        }
    }
}
