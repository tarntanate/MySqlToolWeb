using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Commands.CreateCampaignItemAsset
{
    public class CreateCampaignItemAssetCommandValidator : AbstractValidator<CreateCampaignItemAssetCommand>
    {
        public CreateCampaignItemAssetCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
            RuleFor(p => p.Contact).MaximumLength(5000);
            RuleFor(p => p.Email).Must(BeAValidEmailAddress).WithMessage("Please specify a valid 'Email'.").MaximumLength(20);
            RuleFor(p => p.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Please specify a valid 'PhoneNumber'.").MaximumLength(10);
        }

        private bool BeAValidEmailAddress(string email)
        {
            return email.IsValidEmailAddress();
        }

        private bool BeAValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.IsValidPhoneNumber();
        }
    }
}
