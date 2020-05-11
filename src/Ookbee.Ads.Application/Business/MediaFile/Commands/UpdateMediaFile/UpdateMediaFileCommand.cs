using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UpdateMediaFileCommand()
        {
            Id = string.Empty;
        }

        public UpdateMediaFileCommand(string id, UpdateMediaFileCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
            Contact = request.Contact;
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
        }
    }
}
