using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.IsExistsByIdCampaignPricingModel
{
    public class IsExistsByIdCampaignPricingModelCommandValidator : AbstractValidator<IsExistsByIdCampaignPricingModelCommand>
    {
        public IsExistsByIdCampaignPricingModelCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
