using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkCommandValidator : AbstractValidator<CreateAdNetworkCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdNetworkCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });
        }
    }
}
