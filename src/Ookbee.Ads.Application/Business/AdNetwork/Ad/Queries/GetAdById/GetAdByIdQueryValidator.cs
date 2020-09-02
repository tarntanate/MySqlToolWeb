using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById
{
    public class GetAdByIdQueryValidator : AbstractValidator<GetAdByIdQuery>
    {
        public GetAdByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
