using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.IsExistsAdAssetStatsById
{
    public class IsExistsAdAssetStatsByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdAssetStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
