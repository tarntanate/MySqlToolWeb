using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserMappingProfile : Profile
    {
        public UpdateAdvertiserMappingProfile()
        {
            CreateMap<UpdateAdvertiserCommand, AdvertiserEntity>();
        }
    }
}
