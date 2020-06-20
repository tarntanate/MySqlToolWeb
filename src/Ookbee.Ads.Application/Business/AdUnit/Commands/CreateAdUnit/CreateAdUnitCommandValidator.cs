using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitByName;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdUnitCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.AdUnitTypeId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);

            RuleFor(p => p.AdUnitTypeId)
                .CustomAsync(BeAValidAdUnitTypeId);

            RuleFor(p => p.PublisherId)
                .CustomAsync(BeAValidPublisherId);
        }

        private async Task BeAValidAdUnitTypeId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value));
            if (!result.Ok)
                context.AddFailure(result.Message);
        }

        private async Task BeAValidPublisherId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsPublisherByIdQuery(value));
            if (!result.Ok)
                context.AddFailure(result.Message);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdUnitByNameQuery(value));
            if (!result.Ok)
                context.AddFailure(result.Message);
        }
    }
}
