using System.Threading;
using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommandValidator : AbstractValidator<UpdateCampaignImpressionCommand>
    {
        private IMediator Mediator { get; }

        public UpdateCampaignImpressionCommandValidator(IMediator mediator, CancellationToken cancellation)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Quota)
                .GreaterThan(0);
        }
    }
}
