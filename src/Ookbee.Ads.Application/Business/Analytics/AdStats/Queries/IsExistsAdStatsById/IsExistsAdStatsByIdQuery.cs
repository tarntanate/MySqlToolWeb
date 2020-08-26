using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStatsList.Queries.IsExistsAdStatsById
{
    public class IsExistsAdStatsByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
