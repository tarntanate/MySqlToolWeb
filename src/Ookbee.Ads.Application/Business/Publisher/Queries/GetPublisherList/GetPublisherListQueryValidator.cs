using FluentValidation;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQueryValidator : AbstractValidator<GetPublisherListQuery>
    {
        public GetPublisherListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
