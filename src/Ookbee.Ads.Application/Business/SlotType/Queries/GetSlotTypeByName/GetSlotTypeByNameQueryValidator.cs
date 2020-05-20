using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.SlotType.Queries.GetSlotTypeByName
{
    public class GetSlotTypeByNameQueryValidator : AbstractValidator<GetSlotTypeByNameQuery>
    {
        public GetSlotTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
