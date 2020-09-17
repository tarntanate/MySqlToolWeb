using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Commands.CreateAd
{
    public class CreateAdMappingProfile : Profile
    {
        public CreateAdMappingProfile()
        {
            CreateMap<CreateAdCommand, AdEntity>()
                .ForMember(dest => dest.WebLink, opts => opts.MapFrom(src => src.LinkUrl));
        }
    }
}
