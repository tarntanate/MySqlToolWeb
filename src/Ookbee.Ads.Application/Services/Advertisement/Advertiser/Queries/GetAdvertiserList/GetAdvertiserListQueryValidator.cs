using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.GetAdvertiserList
{
    public class GetAdvertiserListQueryValidator : AbstractValidator<GetAdvertiserListQuery>
    {
        public GetAdvertiserListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
