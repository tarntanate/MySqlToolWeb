using FluentValidation;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListCommandValidator : AbstractValidator<GetCampaignListCommand>
    {
        public GetCampaignListCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
