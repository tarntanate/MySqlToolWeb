using FluentValidation;
using Ookbee.Ads.Common.Extensions;
using System;
using System.Linq;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        public CreateAdCommandValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.CampaignId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.BackgroundColor).Must(BeAValidHexColor).WithMessage("BackgroundColor only support rgb format.");
            RuleFor(p => p.ForegroundColor).Must(BeAValidHexColor).WithMessage("ForegroundColor only support rgb format.");
            RuleFor(p => p.Platforms).Must(value => value != null || value.Count() < 4).WithMessage((rule, value) => $"The length of 'Analytics' must be 3 items or fewer. You entered '{value}' items.");
            RuleFor(p => p.Analytics).Must(value => value != null || value.Count() < 4).WithMessage((rule, value) => $"The length of 'Platforms' must be 3 items or fewer. You entered '{value}' items.");
            RuleForEach(p => p.Analytics).Must(BeAValidUriSchemeHttp).WithMessage((rule, value) => $"Invalid Analytics URL '{value}'");
            RuleForEach(p => p.Platforms).Must(BeAValidPlatform).WithMessage((rule, value) => $"Platforms only support 'Android', 'iOS' and 'Web'");
            RuleFor(p => p.AppLink).MaximumLength(500);
            RuleFor(p => p.WebLink).MaximumLength(500);
        }

        private bool BeAValidHexColor(string value)
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

        private bool BeAValidPlatform(string value)
        {
            if (!value.HasValue())
                return false;

            var platforms = new string[] { "Android", "iOS", "Web" };
            return platforms.Contains(value);
        }
    }
}
