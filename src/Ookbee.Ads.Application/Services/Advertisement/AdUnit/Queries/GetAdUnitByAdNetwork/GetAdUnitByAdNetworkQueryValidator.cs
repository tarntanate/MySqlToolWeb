using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQueryValidator : AbstractValidator<GetAdUnitByAdNetworkQuery>
    {
        public GetAdUnitByAdNetworkQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
