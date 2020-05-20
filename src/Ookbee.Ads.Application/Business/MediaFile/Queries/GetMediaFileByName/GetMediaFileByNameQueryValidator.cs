using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQueryValidator : AbstractValidator<GetMediaFileByNameQuery>
    {
        public GetMediaFileByNameQueryValidator()
        {
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"AdId '{p.AdId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotEmpty();
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
