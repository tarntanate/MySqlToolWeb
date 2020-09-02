using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.GetAdUnitTypeByName;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.IsExistsAdUnitTypeById;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommandValidator : AbstractValidator<UpdateAdUnitTypeCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdUnitTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdUnitTypeCommand;
                    var result = await Mediator.Send(new GetAdUnitTypeByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
