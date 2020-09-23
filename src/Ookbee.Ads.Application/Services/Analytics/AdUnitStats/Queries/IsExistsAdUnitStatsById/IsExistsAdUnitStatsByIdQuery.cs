using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById
{
    public class IsExistsAdUnitStatsByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdUnitStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
