using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsCommandValidator : AbstractValidator<UpdateAdGroupStatsCommand>
    {
        public IMediator Mediator { get; set; }

        public UpdateAdGroupStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdGroupById = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!isExistsAdGroupById.Ok)
                        context.AddFailure(isExistsAdGroupById.Message);
                });

            RuleFor(p => p.Request)
                .GreaterThan(0);
        }
    }
}
