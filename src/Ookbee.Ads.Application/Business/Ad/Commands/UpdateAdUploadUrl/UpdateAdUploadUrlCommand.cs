using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdUploadUrl
{
    public class UpdateAdUploadUrlCommand : IRequest<HttpResult<bool>>
    {
        public string AdId { get; set; }

        public string MediaFileId { get; set; }

        public string UploadFileId { get; set; }

        public UpdateAdUploadUrlCommand()
        {

        }

        public UpdateAdUploadUrlCommand(string adId, UpdateAdUploadUrlCommand request)
        {
            AdId = adId;
            MediaFileId = request.MediaFileId;
            UploadFileId = request.UploadFileId;
        }
    }
}
