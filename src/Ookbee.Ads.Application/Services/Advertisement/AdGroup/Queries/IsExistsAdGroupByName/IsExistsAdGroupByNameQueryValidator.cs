using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQueryValidator : AbstractValidator<IsExistsAdGroupByNameQuery>
    {
        public IsExistsAdGroupByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
