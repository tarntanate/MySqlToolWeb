using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdById
{
    public class CreateCacheAdByIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateCacheAdByIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
