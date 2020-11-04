﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupByName;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeById;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandValidator : AbstractValidator<UpdateAdGroupCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdGroupCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdGroupTypeId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupTypeByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsPublisherByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdGroupCommand;
                    var result = await Mediator.Send(new GetAdGroupByNameQuery(value), cancellationToken);
                    if (result.IsSuccess &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Enabled)
                .NotNull();
        }
    }
}
