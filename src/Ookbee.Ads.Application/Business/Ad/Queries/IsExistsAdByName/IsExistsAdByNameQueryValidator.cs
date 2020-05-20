using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryValidator : AbstractValidator<IsExistsAdByNameQuery>
    {
        public IsExistsAdByNameQueryValidator()
        {
            RuleFor(p => p.AdSlotId).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.AdSlotId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
