﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class DeleteDailySummaryCommandValidator : AbstractValidator<DeleteDailySummaryCommand>
    {
        public DeleteDailySummaryCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"DailySummary '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}