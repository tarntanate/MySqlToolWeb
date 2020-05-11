using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetListAdvertiser
{
    public class GetListAdvertiserCommandValidator : AbstractValidator<GetListAdvertiserCommand>
    {
        public GetListAdvertiserCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
