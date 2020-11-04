using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeById
{
    public class IsExistsAdGroupTypeByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsAdGroupTypeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
