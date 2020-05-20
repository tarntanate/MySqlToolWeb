using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
    {
        public DeletePublisherCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Publisher '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
