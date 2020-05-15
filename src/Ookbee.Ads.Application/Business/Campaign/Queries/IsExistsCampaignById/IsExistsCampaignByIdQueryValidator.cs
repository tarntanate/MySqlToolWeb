using FluentValidation;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryValidator : AbstractValidator<IsExistsCampaignByIdQuery>
    {
        public IsExistsCampaignByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
