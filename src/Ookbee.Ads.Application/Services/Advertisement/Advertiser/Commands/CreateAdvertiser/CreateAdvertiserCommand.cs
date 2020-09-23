using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : IRequest<Response<long>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Contact { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public CreateAdvertiserCommand(CreateAdvertiserRequest request)
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
