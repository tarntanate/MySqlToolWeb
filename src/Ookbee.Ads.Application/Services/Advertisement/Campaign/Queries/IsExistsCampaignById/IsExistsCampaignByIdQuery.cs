using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public IsExistsCampaignByIdQuery(long id)
        {
            Id = id;
        }
    }
}
