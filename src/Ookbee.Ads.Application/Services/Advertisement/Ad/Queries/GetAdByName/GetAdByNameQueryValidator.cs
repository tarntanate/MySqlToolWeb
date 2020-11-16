using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryValidator : AbstractValidator<GetAdByNameQuery>
    {
        public GetAdByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
