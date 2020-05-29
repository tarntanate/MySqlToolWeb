using System;
using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Application.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommandValidator : AbstractValidator<UpdateMediaFileCommand>
    {
        public UpdateMediaFileCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Position).Must(BeAValidPosition).WithMessage(p => $"The Position '{p.Position}' is not supported.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidPosition(string value)
        {
            return Enum.TryParse(value, true, out Position position);
        }
    }
}