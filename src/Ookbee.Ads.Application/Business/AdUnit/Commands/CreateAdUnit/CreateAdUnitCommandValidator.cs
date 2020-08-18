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

            RuleFor(p => p.AdGroupId)
                .NotNull();

            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

             RuleFor(p => p.AdNetworkUnitId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            // In one Ad Group cannot have duplicate AdNetwork
            RuleFor(p => new { AdNetwork = p.AdNetwork, AdGroupId = p.AdGroupId })
                .CustomAsync(async (value, context, CancellationToken) =>
                {
                    var IsExistsAdNetworkNameByAdGroup = await Mediator.Send(new IsExistsAdNetworkNameByAdGroupQuery(adNetworkName: value.AdNetwork, adGroupId: value.AdGroupId), CancellationToken);
                    if (IsExistsAdNetworkNameByAdGroup.Ok)
                        context.AddFailure($"'{value.AdNetwork}' already exists in groupId {value.AdGroupId}.");
                });          
        }
    }
}
