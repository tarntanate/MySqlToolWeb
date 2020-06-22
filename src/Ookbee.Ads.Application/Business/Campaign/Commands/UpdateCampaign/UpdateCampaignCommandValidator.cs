using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Common;
using Ookbee.Ads.Infrastructure.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
    {
        private IMediator Mediator { get; }

        public UpdateCampaignCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now.Date);

            RuleFor(p => p.EndDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now.Date);

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);

            RuleFor(p => p.AdvertiserId)
                .CustomAsync(BeValidAdvertiserId);

            RuleFor(p => p.PricingModel)
                .CustomAsync(BeValidPricingModel);
        }

        private async Task BeValidAdvertiserId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value), cancellationToken);
            if (!result.Ok)
                context.AddFailure(result.Message);
        }

        private async Task BeValidPricingModel(PricingModel value, CustomContext context, CancellationToken cancellationToken)
        {
            var validate = context.InstanceToValidate as UpdateCampaignCommand;
            if (validate.Id == 0)
                context.AddFailure($"Campaign '{validate.Id}' doesn't exist.");

            var result = await Mediator.Send(new GetCampaignByIdQuery(validate.Id), cancellationToken);
            if (!result.Ok)
                context.AddFailure(result.Message);

            if (result.Ok && result.Data.PricingModel != validate.PricingModel)
                context.AddFailure($"You don't have permission to change the Pricing Model.");
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var validate = context.InstanceToValidate as UpdateCampaignCommand;
            var result = await Mediator.Send(new GetCampaignByNameQuery(value));
            if (result.Ok &&
                result.Data.Id != validate.Id &&
                result.Data.Name == value)
                context.AddFailure($"Campaign '{value}' already exists.");
        }
    }
}
