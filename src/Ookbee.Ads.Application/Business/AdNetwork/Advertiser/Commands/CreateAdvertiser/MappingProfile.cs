using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Commands.CreateAdvertiser
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAdvertiserCommand, AdvertiserEntity>();
        }
    }
}
