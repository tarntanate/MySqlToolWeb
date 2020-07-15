using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.User.Queries.IsExistsUserById;

namespace Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog
{
    public class CreateActivityLogCommandValidator : AbstractValidator<CreateActivityLogCommand>
    {
        private IMediator Mediator { get; }

        public CreateActivityLogCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.UserId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsUserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.ObjectId)
                .GreaterThan(0);

            RuleFor(p => p.ObjectType)
                .NotNull()
                .NotEmpty();

            // RuleFor(p => p.ObjectData)
            //     .NotNull()
            //     .NotEmpty();
        }
    }
}
