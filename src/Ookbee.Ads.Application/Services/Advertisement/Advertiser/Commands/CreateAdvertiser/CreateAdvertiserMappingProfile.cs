using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserMappingProfile : Profile
    {
        public CreateAdvertiserMappingProfile()
        {
            CreateMap<CreateAdvertiserCommand, AdvertiserEntity>();
        }
    }
}
