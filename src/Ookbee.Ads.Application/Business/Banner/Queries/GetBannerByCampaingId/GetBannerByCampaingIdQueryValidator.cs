using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByCampaingId
{
    public class GetBannerByCampaingIdQueryValidator : AbstractValidator<GetBannerByCampaingIdQuery>
    {
        public GetBannerByCampaingIdQueryValidator()
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
