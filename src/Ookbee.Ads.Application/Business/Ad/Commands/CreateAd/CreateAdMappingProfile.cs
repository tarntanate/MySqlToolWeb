using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdMappingProfile : Profile
    {
        public CreateAdMappingProfile()
        {
            CreateMap<CreateAdCommand, AdEntity>();
        }
    }
}
