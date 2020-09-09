using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.InitialAssetAdStats
{
    public class InitialAdStatsCommandValidator : AbstractValidator<InitialAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetAdByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}
