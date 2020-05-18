using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotByName
{
    public class GetAdByIdQueryValidator : AbstractValidator<GetAdSlotByNameQuery>
    {
        public GetAdByIdQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
