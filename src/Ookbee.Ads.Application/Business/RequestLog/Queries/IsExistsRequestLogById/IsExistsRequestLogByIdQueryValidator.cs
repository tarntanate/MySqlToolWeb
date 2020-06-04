﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.RequestLog.Queries.IsExistsRequestLogById
{
    public class IsExistsRequestLogByIdQueryValidator : AbstractValidator<IsExistsRequestLogByIdQuery>
    {
        public IsExistsRequestLogByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}