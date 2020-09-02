using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.IsExistsAdNetworkNameByAdGroup
{
    public class IsExistsAdNetworkNameByAdGroupQueryValidator : AbstractValidator<IsExistsAdNetworkNameByAdGroupQuery>
    {
        public IsExistsAdNetworkNameByAdGroupQueryValidator()
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
