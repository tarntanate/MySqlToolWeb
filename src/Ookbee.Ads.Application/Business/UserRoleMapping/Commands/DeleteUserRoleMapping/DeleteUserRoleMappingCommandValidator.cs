using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.DeleteUserRoleMapping
{
    public class DeleteUserRoleMappingCommandValidator : AbstractValidator<DeleteUserRoleMappingCommand>
    {
        public DeleteUserRoleMappingCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
