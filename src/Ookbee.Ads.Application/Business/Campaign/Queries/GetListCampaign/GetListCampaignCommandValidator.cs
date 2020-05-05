using FluentValidation;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetListCampaign
{
    public class GetListCampaignCommandValidator : AbstractValidator<GetListCampaignCommand>
    {
        public GetListCampaignCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
