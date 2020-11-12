using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.IsExistsUserPermissionById
{
    public class IsExistsUserPermissionByIdQueryValidator : AbstractValidator<IsExistsUserPermissionByIdQuery>
    {
        public IsExistsUserPermissionByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
