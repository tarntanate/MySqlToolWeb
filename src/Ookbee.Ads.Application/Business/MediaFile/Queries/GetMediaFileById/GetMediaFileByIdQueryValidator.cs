using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileById
{
    public class GetMediaFileByIdQueryValidator : AbstractValidator<GetMediaFileByIdQuery>
    {
        public GetMediaFileByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
