﻿using System;
using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommandValidator : AbstractValidator<UpdateMediaFileCommand>
    {
        public UpdateMediaFileCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.CampaignId).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.CampaignId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.MimeType).NotEmpty().NotNull().Must(BeAValidMediaType).WithMessage(p => $"The MediaType '{p.MimeType}' is not supported.");
            RuleFor(p => p.AppLink).Must(BeAValidUri).WithMessage(p => $"The AppLink '{p.AppLink}' is not supported.");
            RuleFor(p => p.WebLink).Must(BeAValidUri).WithMessage(p => $"The WebLink '{p.WebLink}' is not supported.");
            RuleFor(p => p.Position).Must(BeAValidPosition).WithMessage(p => $"The Position '{p.Position}' is not supported.");
        }

        private bool BeAValidMediaType(string value)
        {
            return value == MimeTypes.Image.Jpeg || value == MimeTypes.Video.Mpeg;
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }

        private bool BeAValidPosition(string value)
        {
            return Enum.TryParse(value, true, out Position position);
        }

        private bool BeAValidUri(string value)
        {
            return true;
        }
    }
}