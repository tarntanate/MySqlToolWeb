using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.CreateAd
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAdCommand, AdEntity>()
                .ForMember(dest => dest.WebLink, opts => opts.MapFrom(src => src.LinkUrl));
        }
    }
}
