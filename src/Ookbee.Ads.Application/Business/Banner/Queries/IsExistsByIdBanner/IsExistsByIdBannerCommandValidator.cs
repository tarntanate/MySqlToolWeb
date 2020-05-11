using FluentValidation;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsByIdMediaFile
{
    public class IsExistsByIdBannerCommandValidator : AbstractValidator<IsExistsByIdMediaFileCommand>
    {
        public IsExistsByIdBannerCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
