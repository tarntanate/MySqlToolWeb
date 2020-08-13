using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByAdNetwork;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommandValidator : AbstractValidator<UpdateAdUnitCommand>
    {
        private IMediator Mediator { get; }

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

            RuleFor(p => new { p.Id, p.AdNetwork })
                .CustomAsync(async (value, context, CancellationToken) =>
                {
                    var getAdUnitByAdNetwork = await Mediator.Send(new GetAdUnitByAdNetworkQuery(value.AdNetwork), CancellationToken);
                    if (getAdUnitByAdNetwork.Ok &&
                        getAdUnitByAdNetwork.Data.Id != value.Id &&
                        getAdUnitByAdNetwork.Data.AdNetwork != value.AdNetwork)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.AdNetworkUnitId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
