using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQueryValidator : AbstractValidator<GetMediaFileByNameQuery>
    {
        public GetMediaFileByNameQueryValidator()
        {
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.AdId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
