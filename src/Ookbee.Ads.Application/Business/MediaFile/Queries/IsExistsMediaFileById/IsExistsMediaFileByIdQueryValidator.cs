using FluentValidation;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsMediaFileById
{
    public class IsExistsMediaFileByIdQueryValidator : AbstractValidator<IsExistsMediaFileByIdQuery>
    {
        public IsExistsMediaFileByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
