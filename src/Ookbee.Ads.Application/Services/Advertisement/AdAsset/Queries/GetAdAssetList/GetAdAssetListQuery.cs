using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQuery : IRequest<Response<IEnumerable<AdAssetDto>>>
    {
        public int Start { get; private set; }
        public int Length { get; private set; }
        public long? AdId { get; private set; }

        public GetAdAssetListQuery(int start, int length, long? adId)
        {
            Start = start;
            Length = length;
            AdId = adId;
        }
    }
}
