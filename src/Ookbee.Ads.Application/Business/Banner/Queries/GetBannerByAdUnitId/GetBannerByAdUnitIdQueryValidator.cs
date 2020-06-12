using System.Linq;
using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId
{
    public class GetBannerByAdUnitIdQueryValidator : AbstractValidator<GetBannerByAdUnitIdQuery>
    {
        public GetBannerByAdUnitIdQueryValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.AppCode).NotNull().NotEmpty();
            RuleFor(p => p.AppVersion).NotNull().NotEmpty();
            RuleFor(p => p.Platform).Must(BeAValidPlatform).WithMessage($"Platforms only support 'Android', 'iOS' and 'Web'");
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
