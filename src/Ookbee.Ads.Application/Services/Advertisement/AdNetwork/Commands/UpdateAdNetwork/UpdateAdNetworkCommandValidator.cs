using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform;
using Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommandValidator : AbstractValidator<UpdateAdNetworkCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdNetworkCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdNetworkByIdQuery(value), cancellationToken);
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

            RuleFor(p => new { p.AdUnitId, p.Platform })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var command = context.InstanceToValidate as UpdateAdNetworkCommand;
                    var result = await Mediator.Send(new GetAdNetworkByPlatformQuery(value.AdUnitId, value.Platform), cancellationToken);
                    if (result.IsSuccess &&
                        result.Data.Id != command.Id &&
                        result.Data.AdUnitId == value.AdUnitId &&
                        result.Data.Platform == value.Platform)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });
        }
    }
}
