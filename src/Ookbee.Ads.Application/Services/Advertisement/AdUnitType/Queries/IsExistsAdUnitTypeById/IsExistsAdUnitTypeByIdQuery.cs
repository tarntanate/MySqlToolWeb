using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById
{
    public class IsExistsAdUnitTypeByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdUnitTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
