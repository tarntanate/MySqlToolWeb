using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Extensions;
using System.Linq;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!isExistsAdUnitResult.Ok)
                        context.AddFailure(isExistsAdUnitResult.Message);
                });

            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(value), cancellationToken);
                    if (!isExistsCampaignResult.Ok)
                        context.AddFailure(isExistsCampaignResult.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Status)
                .NotNull();

            RuleFor(p => p.BackgroundColor)
                .Must(value => !value.HasValue() || value.IsValidRGBHexColor())
                .WithMessage("'{PropertyName}' is not valid RGB color");

            RuleFor(p => p.ForegroundColor)
                .Must(value => !value.HasValue() || value.IsValidRGBHexColor())
                .WithMessage("'{PropertyName}' is not valid RGB color");

            RuleForEach(p => p.Analytics)
                .Must(value => value.HasValue() && value.IsValidHttp())
                .WithMessage("'{PropertyName}' is not valid HTTP(S) address");

            RuleFor(p => p.Platforms)
                .NotNull()
                .Must(value => value.Count() <= 3)
                .WithMessage("'{PropertyName}' must be 3 items or fewer");

            RuleFor(p => p.AppLink)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.LinkUrl)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidHttp())
                .WithMessage("'{PropertyName}' address is not valid");
        }
    }
}
