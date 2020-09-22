using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommand : UpdateAdvertiserRequest, IRequest<Response<bool>>
    {
        public long Id { get; set; }

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
