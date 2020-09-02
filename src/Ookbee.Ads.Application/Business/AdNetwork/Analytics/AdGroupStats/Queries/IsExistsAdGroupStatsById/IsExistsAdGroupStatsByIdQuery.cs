using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStatsList.Queries.IsExistsAdGroupStatsById
{
    public class IsExistsAdGroupStatsByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdGroupStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
