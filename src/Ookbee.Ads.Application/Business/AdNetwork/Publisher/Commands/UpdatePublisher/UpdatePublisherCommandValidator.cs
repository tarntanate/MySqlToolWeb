using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.IsExistsPublisherById;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
    {
        private IMediator Mediator { get; }

        public UpdatePublisherCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
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
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdatePublisherCommand;
                    var result = await Mediator.Send(new GetPublisherByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.ImagePath)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");
        }
    }
}
