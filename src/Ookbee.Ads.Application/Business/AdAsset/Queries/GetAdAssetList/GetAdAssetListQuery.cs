using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQuery : IRequest<HttpResult<IEnumerable<AdAssetDto>>>
    {
        public long? AdId { get; set; }
        public long? PublisherId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public GetAdAssetListQuery(int start, int length, long adUnitTypeId, long publisherId)
        {
            Start = start;
            Length = length;
            AdId = adUnitTypeId;
            PublisherId = publisherId;
        }
    }
}
