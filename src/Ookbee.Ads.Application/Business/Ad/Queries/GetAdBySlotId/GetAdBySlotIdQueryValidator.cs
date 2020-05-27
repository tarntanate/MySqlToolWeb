using System;
using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Application.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdBySlotId
{
    public class GetAdBySlotIdQueryValidator : AbstractValidator<GetAdBySlotIdQuery>
    {
        public GetAdBySlotIdQueryValidator()
        {
            RuleFor(p => p.AdSlotId).Must(BeAValidObjectId).WithMessage(p => $"AdSlot '{p.AdSlotId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.AppCode).NotNull().NotEmpty();
            RuleFor(p => p.AppVersion).NotNull().NotEmpty();
            RuleFor(p => p.DeviceId).NotNull().NotEmpty();
            RuleFor(p => p.Platform).Must(BeAValidPlatform).WithMessage(p => $"The Platform '{p.Platform}' is not supported.");
            RuleFor(p => p.UserAgent).NotNull().NotEmpty();
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidPlatform(string value)
        {
            return Enum.TryParse(value, true, out Platform position);
        }
    }
}
