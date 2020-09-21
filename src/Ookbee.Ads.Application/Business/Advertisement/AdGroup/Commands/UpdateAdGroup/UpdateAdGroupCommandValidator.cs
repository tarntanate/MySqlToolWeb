using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupByName;
using Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.IsExistsAdGroupById;
using Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.IsExistsPublisherById;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandValidator : AbstractValidator<UpdateAdGroupCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdGroupCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdUnitTypeId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsPublisherByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdGroupCommand;
                    var result = await Mediator.Send(new GetAdGroupByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
