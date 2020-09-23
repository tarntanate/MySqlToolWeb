using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<Response<CampaignDto>>
    {
        public long Id { get; set; }

        public GetCampaignByIdQuery(long id)
        {
            Id = id;
        }
    }
}
