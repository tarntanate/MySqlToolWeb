using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public CreateAdvertiserCommand()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public CreateAdvertiserCommand(CreateAdvertiserCommand request)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
            Contact = request.Contact;
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
        }
    }
}
