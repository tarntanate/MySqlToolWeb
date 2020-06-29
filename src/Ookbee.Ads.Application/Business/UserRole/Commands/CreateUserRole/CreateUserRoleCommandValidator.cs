using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
    {
        private IMediator Mediator { get; }

        public CreateUserRoleCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetUserRoleByNameQuery(value), cancellationToken);
                    if (result.Ok)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
