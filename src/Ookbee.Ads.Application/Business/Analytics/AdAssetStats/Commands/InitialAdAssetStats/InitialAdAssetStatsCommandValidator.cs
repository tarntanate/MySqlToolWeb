using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.InitialAssetAdStats
{
    public class InitialAdAssetStatsCommandValidator : AbstractValidator<InitialAdAssetStatsCommand>
    {
        private IMediator Mediator { get; }

        public InitialAdAssetStatsCommandValidator(IMediator mediator)
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
