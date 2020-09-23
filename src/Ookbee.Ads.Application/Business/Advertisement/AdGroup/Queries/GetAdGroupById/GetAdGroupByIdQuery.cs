using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQuery : IRequest<Response<AdGroupDto>>
    {
        public long Id { get; set; }

        public GetAdGroupByIdQuery(long id)
        {
            Id = id;
        }
    }
}
