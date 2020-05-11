using FluentValidation;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetListBanner
{
    public class GetListBannerCommandValidator : AbstractValidator<GetListBannerCommand>
    {
        public GetListBannerCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
