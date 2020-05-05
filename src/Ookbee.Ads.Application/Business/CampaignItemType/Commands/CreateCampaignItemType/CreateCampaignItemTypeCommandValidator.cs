using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.CreateCampaignItemType
{
    public class CreateCampaignItemTypeCommandValidator : AbstractValidator<CreateCampaignItemTypeCommand>
    {
        public CreateCampaignItemTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }
    }
}
