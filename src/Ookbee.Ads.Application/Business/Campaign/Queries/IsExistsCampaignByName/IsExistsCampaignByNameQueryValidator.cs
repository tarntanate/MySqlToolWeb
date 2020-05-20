using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQueryValidator : AbstractValidator<IsExistsCampaignByNameQuery>
    {
        public IsExistsCampaignByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
