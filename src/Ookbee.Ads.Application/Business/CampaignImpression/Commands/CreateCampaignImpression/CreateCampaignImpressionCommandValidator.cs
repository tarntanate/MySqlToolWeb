using FluentValidation;
using MediatR;
using System.Threading;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommandValidator : AbstractValidator<CreateCampaignImpressionCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignImpressionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Quota)
                .GreaterThan(0);
        }
    }
}
