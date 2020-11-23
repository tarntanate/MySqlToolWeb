using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitIdList
{
    public class GetAdUnitIdListQueryValidator : AbstractValidator<GetAdUnitIdListQuery>
    {
        private readonly IMediator Mediator;

        public GetAdUnitIdListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdGroupResult = await Mediator.Send(new IsExistsAdGroupByIdQuery(value.Value, null), cancellationToken);
                        if (!isExistsAdGroupResult.IsSuccess)
                            context.AddFailure(isExistsAdGroupResult.Message);
                    }
                });
        }
    }
}
