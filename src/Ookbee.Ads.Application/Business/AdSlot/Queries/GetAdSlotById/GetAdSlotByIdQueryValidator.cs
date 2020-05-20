using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById
{
    public class GetAdByIdQueryValidator : AbstractValidator<GetAdSlotByIdQuery>
    {
        public GetAdByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
