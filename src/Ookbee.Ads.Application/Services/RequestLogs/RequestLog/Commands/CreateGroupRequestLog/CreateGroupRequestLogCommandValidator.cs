using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog
{
    public class CreateGroupRequestLogCommandValidator : AbstractValidator<CreateGroupRequestLogCommand>
    {
        private IMediator Mediator { get; }

        public CreateGroupRequestLogCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
