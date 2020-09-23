using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Contact { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public UpdateAdvertiserCommand(long id, UpdateAdvertiserRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
            Contact = request.Contact;
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
        }
    }
}
