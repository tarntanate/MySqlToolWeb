using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog
{
    public class CreateAdClickLogCommandValidator : AbstractValidator<CreateAdClickLogCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdClickLogCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
