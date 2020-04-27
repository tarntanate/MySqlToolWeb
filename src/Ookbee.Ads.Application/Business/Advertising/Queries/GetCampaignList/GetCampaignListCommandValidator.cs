using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertising.Queries.GetCampaignList
{
    public class GetCampaignListCommandValidator : AbstractValidator<GetCampaignListCommand>
    {
        public GetCampaignListCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0);
        }
    }
}
