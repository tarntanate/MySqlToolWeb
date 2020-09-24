using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.InitialAssetAdStatsById
{
    public class InitialAdStatsByIdCommandValidator : AbstractValidator<InitialAdStatsByIdCommand>
    {
        private readonly IMediator Mediator;

        public InitialAdStatsByIdCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetAdByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });
        }
    }
}
