using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList
{
    public class GetAdUnitListQueryValidator : AbstractValidator<GetAdUnitListQuery>
    {
        private IMediator Mediator { get; }

        public GetAdUnitListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdUnitTypeId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .CustomAsync(BeValidAdUnitTypeId);

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .CustomAsync(BeValidPublisherId);
        }

        private async Task BeValidAdUnitTypeId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value.Value));
            if (!isExistsAdUnitResult.Ok)
                context.AddFailure(isExistsAdUnitResult.Message);
        }

        private async Task BeValidPublisherId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            var isExistsPublisherResult = await Mediator.Send(new IsExistsPublisherByIdQuery(value.Value));
            if (!isExistsPublisherResult.Ok)
                context.AddFailure(isExistsPublisherResult.Message);
        }
    }
}
