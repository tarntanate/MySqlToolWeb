using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryValidator : AbstractValidator<IsExistsAdvertiserByIdQuery>
    {
        public IsExistsAdvertiserByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Advertiser '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
