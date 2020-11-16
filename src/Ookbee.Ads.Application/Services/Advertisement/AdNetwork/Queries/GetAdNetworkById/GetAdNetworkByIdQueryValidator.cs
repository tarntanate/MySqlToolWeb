using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQueryValidator : AbstractValidator<GetAdNetworkByIdQuery>
    {
        public GetAdNetworkByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
