using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdUnitTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
