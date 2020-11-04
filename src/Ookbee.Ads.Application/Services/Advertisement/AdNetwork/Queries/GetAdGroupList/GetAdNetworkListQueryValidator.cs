using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkList
{
    public class GetAdNetworkListQueryValidator : AbstractValidator<GetAdNetworkListQuery>
    {
        private readonly IMediator Mediator;

        public GetAdNetworkListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdGroupTypeByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.IsSuccess)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });
        }
    }
}
