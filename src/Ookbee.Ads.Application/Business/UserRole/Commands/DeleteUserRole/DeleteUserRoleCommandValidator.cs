using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandValidator : AbstractValidator<DeleteUserRoleCommand>
    {
        public DeleteUserRoleCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
