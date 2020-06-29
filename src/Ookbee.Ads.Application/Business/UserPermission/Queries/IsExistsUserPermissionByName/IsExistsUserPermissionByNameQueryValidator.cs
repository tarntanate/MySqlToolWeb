using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserPermission.Queries.IsExistsUserPermissionByName
{
    public class IsExistsUserPermissionByNameQueryValidator : AbstractValidator<IsExistsUserPermissionByNameQuery>
    {
        public IsExistsUserPermissionByNameQueryValidator()
        {
            RuleFor(p => p.ExtensionName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
