using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkListByGroupId
{
    public class GetAdNetworkListByGroupIdQueryValidator : AbstractValidator<GetAdNetworkListByGroupIdQuery>
    {
        private readonly IMediator Mediator;

        public GetAdNetworkListByGroupIdQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdGroupById = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!isExistsAdGroupById.IsSuccess)
                        context.AddFailure(isExistsAdGroupById.Message);
                });
        }
    }
}
