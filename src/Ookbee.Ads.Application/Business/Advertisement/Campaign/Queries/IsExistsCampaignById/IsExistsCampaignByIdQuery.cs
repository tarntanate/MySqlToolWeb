using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQuery : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public IsExistsCampaignByIdQuery(long id)
        {
            Id = id;
        }
    }
}
