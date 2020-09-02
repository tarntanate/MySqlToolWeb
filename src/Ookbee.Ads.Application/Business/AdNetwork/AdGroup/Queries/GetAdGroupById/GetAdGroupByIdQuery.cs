using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQuery : IRequest<HttpResult<AdGroupDto>>
    {
        public long Id { get; set; }

        public GetAdGroupByIdQuery(long id)
        {
            Id = id;
        }
    }
}
