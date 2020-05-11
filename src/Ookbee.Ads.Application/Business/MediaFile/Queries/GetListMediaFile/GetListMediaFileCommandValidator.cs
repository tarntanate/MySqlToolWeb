using FluentValidation;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetListMediaFile
{
    public class GetListMediaFileCommandValidator : AbstractValidator<GetListMediaFileCommand>
    {
        public GetListMediaFileCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
