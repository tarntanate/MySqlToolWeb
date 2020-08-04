using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemByName;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemById;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem
{
    public class UpdateAdGroupItemCommandValidator : AbstractValidator<UpdateAdGroupItemCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdGroupItemCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupItemByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0);
                // .CustomAsync(async (value, context, cancellationToken) =>
                // {
                //     var result = await Mediator.Send(new IsExistsAdGroupItemByIdQuery(value), cancellationToken);
                //     if (!result.Ok)
                //         context.AddFailure(result.Message);
                // });

            RuleFor(p => p.AdUnitKey)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdGroupItemCommand;
                    var result = await Mediator.Send(new GetAdGroupItemByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.AdGroupId == validate.AdGroupId &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
