using MediatR;
using Ookbee.Ads.Infrastructure.Enums;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdIdListByPlatform
{
    public class CreateAdIdListByPlatformCommand : IRequest<Unit>
    {
        public long AdId { get; set; }
        public long AdUnitId { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }

        public CreateAdIdListByPlatformCommand(long adId, long adUnitId, IEnumerable<Platform> platforms)
        {
            AdId = adId;
            AdUnitId = adUnitId;
            Platforms = platforms;
        }
    }
}
