using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupById;
using Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Queries.IsExistsAdGroupStatsById;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsCommandValidator : AbstractValidator<UpdateAdStatsCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupStatsByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

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
