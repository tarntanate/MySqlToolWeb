using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryValidator : AbstractValidator<GetCampaignByNameQuery>
    {
        public GetCampaignByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
