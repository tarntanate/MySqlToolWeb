using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdUnitId
{
    public class GetAdUnitIdQuery : IRequest<Response<long>>
    {
        public long Id { get; private set; }

        public GetAdUnitIdQuery(long id)
        {
            Id = id;
        }
    }
}
