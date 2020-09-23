using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQueryValidator : AbstractValidator<IsExistsAdGroupByNameQuery>
    {
        public IsExistsAdGroupByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
