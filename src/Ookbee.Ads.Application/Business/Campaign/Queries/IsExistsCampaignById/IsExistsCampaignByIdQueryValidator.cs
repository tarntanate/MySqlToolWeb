using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryValidator : AbstractValidator<IsExistsCampaignByIdQuery>
    {
        public IsExistsCampaignByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Campaign '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
