using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroup;
using System.Threading;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdUnitCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .NotNull();

            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => new { AdNetwork = p.AdNetwork, AdGroupId = p.AdGroupId })
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var IsExistsAdNetworkNameByAdGroup = await Mediator.Send(new IsExistsAdUnitByAdGroupQuery(adNetworkName: value.AdNetwork, adGroupId: value.AdGroupId), cancellationToken);
                    if (IsExistsAdNetworkNameByAdGroup.Ok)
                        context.AddFailure($"'{value.AdNetwork}' already exists in groupId {value.AdGroupId}.");
                });
        }
    }
}
