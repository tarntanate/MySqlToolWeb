using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerById
{
    public class GetBannerByIdQueryValidator : AbstractValidator<GetBannerByIdQuery>
    {
        public GetBannerByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
