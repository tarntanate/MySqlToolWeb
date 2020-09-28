using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignByName;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        private readonly IMediator Mediator;

        public CreateCampaignCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as CreateCampaignCommand;
                    var result = await Mediator.Send(new GetCampaignByNameQuery(value), cancellationToken);
                    if (result.IsSuccess &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
