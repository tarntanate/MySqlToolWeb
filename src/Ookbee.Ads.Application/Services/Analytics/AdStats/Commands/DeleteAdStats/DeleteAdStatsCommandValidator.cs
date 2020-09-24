using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.DeleteAdStats
{
    public class DeleteAdStatsCommandValidator : AbstractValidator<DeleteAdStatsCommand>
    {
        private readonly IMediator Mediator;

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
