using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByAdId
{
    public class GetAdAssetByAdIdQuery : IRequest<HttpResult<IEnumerable<AdAssetDto>>>
    {
        public long AdId { get; set; }

        public GetAdAssetByAdIdQuery(long adId)
        {
            AdId = adId;
        }
    }
}
