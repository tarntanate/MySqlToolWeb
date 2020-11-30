using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog
{
    public class CreateAdImpressionLogCommandValidator : AbstractValidator<CreateAdImpressionLogCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdImpressionLogCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
