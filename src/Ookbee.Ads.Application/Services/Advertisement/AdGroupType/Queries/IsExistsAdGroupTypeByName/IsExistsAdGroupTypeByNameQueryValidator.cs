using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeByName
{
    public class IsExistsAdGroupTypeByNameQueryValidator : AbstractValidator<IsExistsAdGroupTypeByNameQuery>
    {
        public IsExistsAdGroupTypeByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
