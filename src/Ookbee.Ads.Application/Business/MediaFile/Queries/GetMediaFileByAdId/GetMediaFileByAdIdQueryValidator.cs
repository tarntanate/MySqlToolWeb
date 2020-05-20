using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByAdId
{
    public class GetMediaFileByAdIdQueryValidator : AbstractValidator<GetMediaFileByAdIdQuery>
    {
        public GetMediaFileByAdIdQueryValidator()
        {
            RuleFor(p => p.AdId).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.AdId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
