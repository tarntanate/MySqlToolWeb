using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdById
{
    public class DeleteAdByIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdByIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
