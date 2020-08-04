using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.User.Queries.IsExistsUserById;
using Ookbee.Ads.Common.Extensions;

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
                .Custom((value, context) =>
               {
                   var validate = context.InstanceToValidate as CreateActivityLogCommand;
                   if (validate.ObjectData.HasValue())
                   {
                       if (!value.HasValue())
                       {
                           context.AddFailure("'Object Type' must not be null or empty.");
                       }
                   }
               });
        }
    }
}
