using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.IsExistsUserPermissionByName
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
