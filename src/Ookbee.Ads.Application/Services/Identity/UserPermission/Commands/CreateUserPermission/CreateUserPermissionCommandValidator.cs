using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionByName;
using Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleById;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommandValidator : AbstractValidator<CreateUserPermissionCommand>
    {
        private readonly IMediator Mediator;

        public CreateUserPermissionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.RoleId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetUserRoleByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.ExtensionName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetUserPermissionByNameQuery(value), cancellationToken);
                    if (result.Ok)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });
        }
    }
}
