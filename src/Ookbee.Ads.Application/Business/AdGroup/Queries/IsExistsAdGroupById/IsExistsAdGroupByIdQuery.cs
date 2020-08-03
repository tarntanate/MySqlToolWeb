using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdGroupByIdQuery(long id)
        {
            Id = id;
        }
    }
}
