using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.IsExistsUserRoleById
{
    public class IsExistsUserRoleByIdQueryValidator : AbstractValidator<IsExistsUserRoleByIdQuery>
    {
        public IsExistsUserRoleByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
