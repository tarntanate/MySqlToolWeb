using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByAdNetwork;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdUnitCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdNetwork)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10)
                .CustomAsync(async (value, context, CancellationToken) =>
                {
                    var getAdUnitByAdNetwork = await Mediator.Send(new GetAdUnitByAdNetworkQuery(value), CancellationToken);
                    if (getAdUnitByAdNetwork.Ok &&
                        getAdUnitByAdNetwork.Data.AdNetwork != value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.AdNetworkUnitId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
