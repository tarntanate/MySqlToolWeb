using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.IsExistsAdStatsById
{
    public class IsExistsAdStatsByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdStatsByIdQuery(long id)
        {
            Id = id;
        }
    }
}
