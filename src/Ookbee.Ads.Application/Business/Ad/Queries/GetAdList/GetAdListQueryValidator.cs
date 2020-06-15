using FluentValidation;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryValidator : AbstractValidator<GetAdListQuery>
    {
        public GetAdListQueryValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.AdUnitId != null);
            RuleFor(p => p.CampaignId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.CampaignId != null);
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
