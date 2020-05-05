﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetByIdCampaignAdvertiser
{
    public class GetByIdCampaignAdvertiserCommandValidator : AbstractValidator<GetByIdCampaignAdvertiserCommand>
    {
        public GetByIdCampaignAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).Length(24);
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
