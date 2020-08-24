using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdNetworkNameByAdGroup;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdUnitCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => new { AdNetwork = p.AdNetwork, AdGroupId = p.AdGroupId })
                // .NotNull()
                // .NotEmpty()
                // .MaximumLength(10)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var IsExistsAdNetworkNameByAdGroup = await Mediator.Send(new IsExistsAdNetworkNameByAdGroupQuery(adNetworkName: value.AdNetwork, adGroupId: value.AdGroupId), cancellationToken);
                    if (IsExistsAdNetworkNameByAdGroup.Ok)
                        context.AddFailure($"'{value.AdNetwork}' already exists in groupId {value.AdGroupId}.");
                });

            RuleFor(p => p.AdNetworkUnitId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
