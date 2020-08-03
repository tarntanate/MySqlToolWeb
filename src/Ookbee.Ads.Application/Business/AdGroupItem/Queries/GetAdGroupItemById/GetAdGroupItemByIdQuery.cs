using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemById
{
    public class GetAdGroupItemByIdQuery : IRequest<HttpResult<AdGroupItemDto>>
    {
        public long Id { get; set; }

        public GetAdGroupItemByIdQuery(long id)
        {
            Id = id;
        }
    }
}
