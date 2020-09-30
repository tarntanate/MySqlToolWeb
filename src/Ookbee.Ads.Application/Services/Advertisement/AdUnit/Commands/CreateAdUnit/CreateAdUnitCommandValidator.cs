using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroupId;
using System.Threading;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        private readonly IMediator Mediator;

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
                    var IsExistsAdNetworkNameByAdGroup = await Mediator.Send(new IsExistsAdUnitByGroupIdQuery(adNetworkName: value.AdNetwork, adGroupId: value.AdGroupId), cancellationToken);
                    if (IsExistsAdNetworkNameByAdGroup.IsSuccess)
                        context.AddFailure($"'{value.AdNetwork}' already exists in groupId {value.AdGroupId}.");
                });
        }
    }
}
