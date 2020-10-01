using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroupId
{
    public class IsExistsAdUnitByGroupIdQueryValidator : AbstractValidator<IsExistsAdUnitByGroupIdQuery>
    {
        public IsExistsAdUnitByGroupIdQueryValidator()
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
