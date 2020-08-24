using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.UpdateCampaignImpression
{
    public class UpdateCampaignImpressionCommandValidator : AbstractValidator<UpdateCampaignImpressionCommand>
    {
        private IMediator Mediator { get; }

        public UpdateCampaignImpressionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
