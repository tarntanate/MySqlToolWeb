using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.DeleteMediaFile
{
    public class DeleteMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string AdId { get; set; }

        public DeleteMediaFileCommand()
        {
            
        }

        public DeleteMediaFileCommand(string adId, string id)
        {
            Id = id;
            AdId = adId;
        }
    }
}
