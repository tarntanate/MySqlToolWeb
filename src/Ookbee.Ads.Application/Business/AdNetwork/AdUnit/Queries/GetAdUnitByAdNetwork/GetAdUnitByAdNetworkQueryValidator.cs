using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQueryValidator : AbstractValidator<GetAdUnitByAdNetworkQuery>
    {
        public GetAdUnitByAdNetworkQueryValidator()
        {
            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
