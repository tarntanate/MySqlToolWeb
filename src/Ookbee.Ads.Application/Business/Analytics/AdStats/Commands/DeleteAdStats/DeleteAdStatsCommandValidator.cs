using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.DeleteAdStats
{
    public class DeleteAdStatsCommandValidator : AbstractValidator<DeleteAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public DeleteAdStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}
