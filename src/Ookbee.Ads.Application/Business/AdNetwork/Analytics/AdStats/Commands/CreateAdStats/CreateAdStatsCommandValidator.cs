using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.CreateAdStats
{
    public class CreateAdStatsCommandValidator : AbstractValidator<CreateAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}
