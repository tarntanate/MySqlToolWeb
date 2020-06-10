using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandValidator : AbstractValidator<CreateAdvertiserCommand>
    {
        public CreateAdvertiserCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImagePath).MaximumLength(250);
            RuleFor(p => p.Contact).MaximumLength(5000);
            RuleFor(p => p.Email).Must(BeAValidEmailAddress).WithMessage("Please specify a valid 'Email'.").MaximumLength(30);
            RuleFor(p => p.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Please specify a valid 'PhoneNumber'.").MaximumLength(10);
        }

        private bool BeAValidEmailAddress(string value)
        {
            return value.IsValidEmailAddress();
        }

        private bool BeAValidPhoneNumber(string value)
        {
            return value.IsValidPhoneNumber();
        }
    }
}
