using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupById;
using Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.IsExistsAdAssetStatsById;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.UpdateAdAssetStats
{
    public class UpdateAdAssetStatsCommandValidator : AbstractValidator<UpdateAdAssetStatsCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdAssetStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdAssetStatsByIdQuery(value), cancellationToken);
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
