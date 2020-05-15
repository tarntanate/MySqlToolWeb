using FluentValidation;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById
{
    public class IsExistsBannerByIdQueryValidator : AbstractValidator<IsExistsMediaFileByIdQuery>
    {
        public IsExistsBannerByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
