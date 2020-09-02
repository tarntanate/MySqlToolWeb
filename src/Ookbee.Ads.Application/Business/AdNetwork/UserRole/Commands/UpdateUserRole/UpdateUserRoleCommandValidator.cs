using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleByName;
using Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.IsExistsUserRoleById;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        private IMediator Mediator { get; }

        public UpdateUserRoleCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsUserRoleByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateUserRoleCommand;
                    var result = await Mediator.Send(new GetUserRoleByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
