using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByAdNetwork
{
    public class IsExistsAdUnitByAdNetworkQueryValidator : AbstractValidator<IsExistsAdUnitByAdNetworkQuery>
    {
        public IsExistsAdUnitByAdNetworkQueryValidator()
        {
            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
