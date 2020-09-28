using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryValidator : AbstractValidator<IsExistsAdvertiserByIdQuery>
    {
        public IsExistsAdvertiserByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
