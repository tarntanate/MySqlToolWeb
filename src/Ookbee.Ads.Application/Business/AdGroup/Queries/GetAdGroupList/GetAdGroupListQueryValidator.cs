using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList
{
    public class GetAdGroupListQueryValidator : AbstractValidator<GetAdGroupListQuery>
    {
        public GetAdGroupListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
