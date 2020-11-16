using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryValidator : AbstractValidator<GetAdByIdQuery>
    {
        public GetAdByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
