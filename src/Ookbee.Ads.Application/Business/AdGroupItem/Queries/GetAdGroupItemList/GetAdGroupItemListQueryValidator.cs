using FluentValidation;
using MediatR;

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
                .NotNull()
                .NotEmpty();
        }
    }
}
