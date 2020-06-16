using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQueryValidator : AbstractValidator<GetAdUnitListQuery>
    {
        public GetAdUnitListQueryValidator()
        {
            // RuleFor(p => p.AdUnitTypeId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.AdUnitTypeId != null);
            // RuleFor(p => p.PublisherId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.PublisherId != null);
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
