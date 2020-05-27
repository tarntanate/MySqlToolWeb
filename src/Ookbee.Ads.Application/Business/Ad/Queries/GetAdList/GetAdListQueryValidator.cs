using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryValidator : AbstractValidator<GetAdListQuery>
    {
        public GetAdListQueryValidator()
        {
            RuleFor(p => p.AdSlotId).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.AdSlotId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.CampaignId).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.CampaignId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeAValidObjectId(string value)
        {
            if (value.HasValue())
                return ObjectId.TryParse(value, out ObjectId objectId);
            return true;
        }
    }
}
