using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQuery : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsCampaignByIdQuery(string id)
        {
            Id = id;
        }
    }
}
