﻿using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaingId
{
    public class GetAdByCampaingIdQueryValidator : AbstractValidator<GetAdByCampaingIdQuery>
    {
        public GetAdByCampaingIdQueryValidator()
        {
            RuleFor(p => p.CampaingId).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.CampaingId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}