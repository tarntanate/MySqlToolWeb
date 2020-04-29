using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.GetAdvertiser
{
    public class GetAdvertiserCommandValidator : AbstractValidator<GetAdvertiserCommand>
    {
        public GetAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
