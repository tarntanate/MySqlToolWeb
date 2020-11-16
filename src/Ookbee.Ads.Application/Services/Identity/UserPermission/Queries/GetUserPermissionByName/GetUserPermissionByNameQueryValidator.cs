using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQueryValidator : AbstractValidator<GetUserPermissionByNameQuery>
    {
        public GetUserPermissionByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.ExtensionName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
