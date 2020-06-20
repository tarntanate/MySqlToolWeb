using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommandValidator : AbstractValidator<CommitUploadUrlCommand>
    {
        public CommitUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
