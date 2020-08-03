using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemById
{
    public class IsExistsAdGroupItemByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdGroupItemByIdQuery(long id)
        {
            Id = id;
        }
    }
}
