using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemList
{
    public class GetAdGroupItemListQueryValidator : AbstractValidator<GetAdGroupItemListQuery>
    {
        private IMediator Mediator { get; }

        public GetAdGroupItemListQueryValidator(IMediator mediator)
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
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdGroupByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.Ok)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });
        }
    }
}
