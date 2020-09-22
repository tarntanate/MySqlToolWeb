using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : CreateAdvertiserRequest, IRequest<Response<long>>
    {
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
