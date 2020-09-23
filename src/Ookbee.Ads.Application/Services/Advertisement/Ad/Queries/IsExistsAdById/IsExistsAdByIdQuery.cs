using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsAdByIdQuery(long id)
        {
            Id = id;
        }
    }
}
