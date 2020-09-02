using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Queries.GetUserPermissionByName;
using Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleById;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.CreateUserPermission
{
    public class CreateUserPermissionCommandValidator : AbstractValidator<CreateUserPermissionCommand>
    {
        private IMediator Mediator { get; }

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
