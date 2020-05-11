using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetListMediaFile
{
    public class GetListMediaFileCommand : IRequest<HttpResult<IEnumerable<MediaFileDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public GetListMediaFileCommand(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
