using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeByName;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.UpdateAdGroupType
{
    public class UpdateAdGroupTypeCommandValidator : AbstractValidator<UpdateAdGroupTypeCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdGroupTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupTypeByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var command = context.InstanceToValidate as UpdateAdGroupTypeCommand;
                    var result = await Mediator.Send(new GetAdGroupTypeByNameQuery(value), cancellationToken);
                    if (result.IsSuccess &&
                        result.Data.Id != command.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
