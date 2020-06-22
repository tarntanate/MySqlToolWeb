using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

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

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);

            RuleFor(p => p.AdvertiserId)
                .CustomAsync(BeValidAdvertiserId);
        }

        private async Task BeValidAdvertiserId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value), cancellationToken);
            if (!result.Ok)
                context.AddFailure(result.Message);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var validate = context.InstanceToValidate as CreateCampaignCommand;
            var result = await Mediator.Send(new GetCampaignByNameQuery(value));
            if (result.Ok &&
                result.Data.Name == value)
                context.AddFailure($"Campaign '{value}' already exists.");
        }
    }
}
