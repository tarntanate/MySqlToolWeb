using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.CreateCampaignPricingModel
{
    public class CreateCampaignPricingModelCommandValidator : AbstractValidator<CreateCampaignPricingModelCommand>
    {
        public CreateCampaignPricingModelCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
