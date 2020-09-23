using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQuery : IRequest<Response<IEnumerable<AdAssetDto>>>
    {
        public long? AdId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdAssetListQuery(int start, int length, long? adId)
        {
            Start = start;
            Length = length;
            AdId = adId;
        }
    }
}
