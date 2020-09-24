using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitByAdNetwork;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommandValidator : AbstractValidator<UpdateAdUnitCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdUnitCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => new { p.Id, p.AdNetwork, p.AdGroupId })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var getAdUnitByAdNetwork = await Mediator.Send(new GetAdUnitByAdNetworkQuery(value.AdNetwork), cancellationToken);
                    if (getAdUnitByAdNetwork.Ok &&
                        getAdUnitByAdNetwork.Data.Id != value.Id &&
                        getAdUnitByAdNetwork.Data.AdGroup.Id == value.AdGroupId)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

        }
    }
}
