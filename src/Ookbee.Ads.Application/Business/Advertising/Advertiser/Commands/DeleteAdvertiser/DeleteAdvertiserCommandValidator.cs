using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandValidator : AbstractValidator<DeleteAdvertiserCommand>
    {
        public DeleteAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
