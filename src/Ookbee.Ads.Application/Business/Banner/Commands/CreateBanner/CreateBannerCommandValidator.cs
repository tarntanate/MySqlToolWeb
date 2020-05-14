using System;
using FluentValidation;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Banner.Commands.CreateBanner
{
    public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
    {
        public CreateBannerCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.Cooldown).GreaterThan(TimeSpan.FromSeconds(0)).LessThanOrEqualTo(TimeSpan.FromSeconds(30));
            RuleFor(p => p.BackgroundColor).Must(BeARGBHexColor).WithMessage("BackgroundColor only support rgb format.");
            RuleFor(p => p.ForegroundColor).Must(BeARGBHexColor).WithMessage("ForegroundColor only support rgb format.");
        }

        private bool BeARGBHexColor(string value)
        {
            return value.IsValidRGBHexColor();
        }
    }
}
