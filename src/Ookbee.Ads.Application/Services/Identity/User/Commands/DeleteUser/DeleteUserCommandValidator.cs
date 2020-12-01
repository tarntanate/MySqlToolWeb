using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Identity.User.Queries.IsExistsUserById;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Services.Identity.User.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        private readonly IMediator Mediator;

        public DeleteUserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsUserByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });
        }
    }
}
