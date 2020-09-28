using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Services.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsCommandValidator : AbstractValidator<UpdateAdUnitStatsCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdUnitStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitStatsByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });
        }
    }
}
