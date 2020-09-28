using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryValidator : AbstractValidator<GetAdUnitByIdQuery>
    {
        public GetAdUnitByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
