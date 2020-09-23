using FluentValidation;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQueryValidator : AbstractValidator<IsExistsUserRoleByNameQuery>
    {
        public IsExistsUserRoleByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
