using FluentValidation;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrl
{
    public class GetByIdUploadUrlCommandValidator : AbstractValidator<GetByIdUploadUrlCommand>
    {
        public GetByIdUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
