using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : IRequest<HttpResult<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public CreateAdvertiserCommand()
        {

        }

        public CreateAdvertiserCommand(CreateAdvertiserCommand request)
        {
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
            Contact = request.Contact;
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
        }
    }
}
