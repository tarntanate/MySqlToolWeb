﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById
{
    public class IsExistsAdByIdQueryValidator : AbstractValidator<IsExistsMediaFileByIdQuery>
    {
        public IsExistsAdByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
