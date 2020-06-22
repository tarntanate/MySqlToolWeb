using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByName;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById;
using System.Threading;
using System.Threading.Tasks;

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
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.AdUnitTypeId)
                .CustomAsync(BeAValidAdUnitTypeId);

            RuleFor(p => p.PublisherId)
                .CustomAsync(BeAValidPublisherId);

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);
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
            var validate = context.InstanceToValidate as UpdateAdUnitCommand;
            if (validate.Id == 0)
                context.AddFailure($"AdUnit '{validate.Id}' doesn't exist.");

            var result = await Mediator.Send(new GetAdUnitByNameQuery(value));
            if (result.Ok &&
                result.Data.Id != validate.Id &&
                result.Data.Name == value)
                context.AddFailure($"AdUnit '{value}' already exists.");
        }
    }
}
