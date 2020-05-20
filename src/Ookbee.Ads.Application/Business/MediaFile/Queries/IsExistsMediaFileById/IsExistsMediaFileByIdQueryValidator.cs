using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById
{
    public class IsExistsMediaFileByIdQueryValidator : AbstractValidator<IsExistsMediaFileByIdQuery>
    {
        public IsExistsMediaFileByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"MediaFile '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
