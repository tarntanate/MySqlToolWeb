using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdNetwork
{
    public class IsExistsAdUnitByAdNetworkQueryValidator : AbstractValidator<IsExistsAdUnitByAdNetworkQuery>
    {
        public IsExistsAdUnitByAdNetworkQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
