using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById;
using Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommandValidator : AbstractValidator<UpdateAdNetworkCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdNetworkCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdNetworkByIdQuery(value), cancellationToken);
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

            RuleFor(p => p.Platform)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdNetworkCommand;
                    var result = await Mediator.Send(new GetAdNetworkByPlatformQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Platform == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });
        }
    }
}
