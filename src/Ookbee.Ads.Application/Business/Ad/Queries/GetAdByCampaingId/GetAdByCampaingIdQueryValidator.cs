using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByCampaignId
{
    public class GetAdByCampaignIdQueryValidator : AbstractValidator<GetAdByCampaignIdQuery>
    {
        public GetAdByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.CampaignId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
