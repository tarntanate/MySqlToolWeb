using FluentValidation;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsByIdMediaFile
{
    public class IsExistsByIdMediaFileCommandValidator : AbstractValidator<IsExistsByIdMediaFileCommand>
    {
        public IsExistsByIdMediaFileCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
