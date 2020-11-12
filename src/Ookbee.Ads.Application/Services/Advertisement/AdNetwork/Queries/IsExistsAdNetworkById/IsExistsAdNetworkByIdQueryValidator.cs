using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById
{
    public class IsExistsAdNetworkByIdQueryValidator : AbstractValidator<IsExistsAdNetworkByIdQuery>
    {
        public IsExistsAdNetworkByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
