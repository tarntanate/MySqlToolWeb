using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdById
{
    public class CreateAdByIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateAdByIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
