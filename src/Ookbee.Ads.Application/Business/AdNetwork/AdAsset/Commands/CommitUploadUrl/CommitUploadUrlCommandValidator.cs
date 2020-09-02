using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommandValidator : AbstractValidator<CommitUploadUrlCommand>
    {
        public CommitUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
