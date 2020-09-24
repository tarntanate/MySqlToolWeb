using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroup
{
    public class IsExistsAdUnitByAdGroupQueryValidator : AbstractValidator<IsExistsAdUnitByAdGroupQuery>
    {
        public IsExistsAdUnitByAdGroupQueryValidator()
        {
            RuleFor(p => p.AdNetworkName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
            
            RuleFor(p => p.AdGroupId)
                .NotNull();
        }
    }
}
