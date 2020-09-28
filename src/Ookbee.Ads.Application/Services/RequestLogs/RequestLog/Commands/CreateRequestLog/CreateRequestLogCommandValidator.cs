using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogCommandValidator : AbstractValidator<CreateRequestLogCommand>
    {
        private IMediator Mediator { get; }

        public CreateRequestLogCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
