using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : CreateAdvertiserRequest, IRequest<HttpResult<long>>
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
