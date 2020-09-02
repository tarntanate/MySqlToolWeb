using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommandValidator : AbstractValidator<CreateCampaignImpressionCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignImpressionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
