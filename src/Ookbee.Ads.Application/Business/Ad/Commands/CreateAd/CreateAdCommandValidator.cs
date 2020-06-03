using System;
using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        public CreateAdCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.Cooldown).GreaterThan(TimeSpan.FromSeconds(0)).LessThanOrEqualTo(TimeSpan.FromSeconds(30));
            RuleFor(p => p.BackgroundColor).Must(BeARGBHexColor).WithMessage("BackgroundColor only support rgb format.");
            RuleFor(p => p.ForegroundColor).Must(BeARGBHexColor).WithMessage("ForegroundColor only support rgb format.");
            RuleForEach(p => p.Analytics).Must(BeAValidUriSchemeHttp).WithMessage((rule, value) => $"Invalid Analytics URL '{value}'");
            RuleFor(p => p.AppLink).NotEmpty().NotEmpty().MaximumLength(250).Must(BeAValidUri).WithMessage(p => $"Invalid AppLink URL '{p.AppLink}'");
            RuleFor(p => p.WebLink).NotEmpty().NotEmpty().MaximumLength(250).Must(BeAValidUriSchemeHttp).WithMessage(p => $"Invalid WebLink URL '{p.WebLink}'");
            RuleFor(p => p.Platform).NotNull();
        }

        private bool BeARGBHexColor(string value)
        {
            if (value.HasValue())
                return value.IsValidRGBHexColor();
            return true;
        }

        private bool BeAValidUri(string value)
        {
            return value.IsValidUri();
        }

        private bool BeAValidUriSchemeHttp(string value)
        {
            return value.IsValidUriSchemeHttp();
        }
    }
}
