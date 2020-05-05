using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetListCampaignPricingModel
{
    public class GetListCampaignPricingModelCommandValidator : AbstractValidator<GetListCampaignPricingModelCommand>
    {
        public GetListCampaignPricingModelCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
