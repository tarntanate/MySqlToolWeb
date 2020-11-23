using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQueryValidator : AbstractValidator<GetAdUnitListQuery>
    {
        private readonly IMediator Mediator;

        public GetAdUnitListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0);
        }
    }
}
