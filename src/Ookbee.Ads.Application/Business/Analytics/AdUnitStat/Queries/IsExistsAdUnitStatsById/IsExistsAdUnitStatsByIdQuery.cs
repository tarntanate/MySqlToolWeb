using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.IsExistsAdUnitStatsById
{
    public class IsExistsAdUnitStatsByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdUnitStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
