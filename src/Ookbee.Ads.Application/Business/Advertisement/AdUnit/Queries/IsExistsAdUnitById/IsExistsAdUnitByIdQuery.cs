using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsAdUnitByIdQuery(long id)
        {
            Id = id;
        }
    }
}
