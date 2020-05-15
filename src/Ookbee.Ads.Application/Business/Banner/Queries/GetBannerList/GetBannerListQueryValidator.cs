using FluentValidation;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerList
{
    public class GetBannerListQueryValidator : AbstractValidator<GetBannerListQuery>
    {
        public GetBannerListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
