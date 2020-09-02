using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign
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
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsCampaignByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateCampaignCommand;
                    var result = await Mediator.Send(new GetCampaignByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.StartDate)
               .CustomAsync(async (value, context, cancellationToken) =>
               {
                   var validate = context.InstanceToValidate as UpdateCampaignCommand;
                   var result = await Mediator.Send(new GetCampaignByIdQuery(validate.Id), cancellationToken);
                   if (!result.Ok)
                       context.AddFailure(result.Message);

                   if (result.Ok && result.Data.StartDate.ToUniversalTime() != validate.StartDate.ToUniversalTime())
                   {
                       if (validate.StartDate.ToUniversalTime() <= MechineDateTime.UtcNow)
                           context.AddFailure($"Campaign 'Start Date' must greater than current time");
                   }
               });

            RuleFor(p => p.EndDate)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateCampaignCommand;
                    var result = await Mediator.Send(new GetCampaignByIdQuery(validate.Id), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);

                    if (result.Ok && result.Data.EndDate.ToUniversalTime() != validate.EndDate.ToUniversalTime())
                    {
                        if (validate.EndDate.ToUniversalTime() <= MechineDateTime.UtcNow)
                            context.AddFailure($"Campaign 'End Date' must greater than current time");
                        if (validate.EndDate <= result.Data.StartDate)
                            context.AddFailure($"Campaign 'End Date' must greater than campaign's start date");
                    }
                });
        }
    }
}
