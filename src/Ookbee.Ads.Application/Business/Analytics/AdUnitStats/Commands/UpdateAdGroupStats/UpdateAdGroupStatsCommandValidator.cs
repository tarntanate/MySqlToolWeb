using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommandValidator : AbstractValidator<UpdateAdUnitStatsCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdUnitStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitStatsByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}
