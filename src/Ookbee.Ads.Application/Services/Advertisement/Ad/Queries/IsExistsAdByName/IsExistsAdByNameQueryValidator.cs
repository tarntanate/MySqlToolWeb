using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryValidator : AbstractValidator<IsExistsAdByNameQuery>
    {
        public IsExistsAdByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
