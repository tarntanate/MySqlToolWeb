using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByBannerId
{
    public class GetMediaFileByBannerIdQueryValidator : AbstractValidator<GetMediaFileByBannerIdQuery>
    {
        public GetMediaFileByBannerIdQueryValidator()
        {
            RuleFor(p => p.BannerId).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.BannerId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
