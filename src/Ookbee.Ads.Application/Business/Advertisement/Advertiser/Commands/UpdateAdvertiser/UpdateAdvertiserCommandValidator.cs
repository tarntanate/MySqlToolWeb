using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandValidator : AbstractValidator<UpdateAdvertiserCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdvertiserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdvertiserCommand;
                    var result = await Mediator.Send(new GetAdvertiserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdvertiserCommand;
                    var result = await Mediator.Send(new GetAdvertiserByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Contact)
                .MaximumLength(5000);

            RuleFor(p => p.ImagePath)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.Email)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidEmailAddress())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(10)
                .Must(value => !value.HasValue() || value.IsValidPhoneNumber())
                .WithMessage("'{PropertyName}' is not valid");
        }
    }
}
