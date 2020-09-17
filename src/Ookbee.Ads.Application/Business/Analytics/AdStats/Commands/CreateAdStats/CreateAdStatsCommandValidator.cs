using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommandValidator : AbstractValidator<CreateAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdStatsCommandValidator(IMediator mediator)
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
