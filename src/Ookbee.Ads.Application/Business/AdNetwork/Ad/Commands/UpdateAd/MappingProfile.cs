using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.UpdateAd
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdCommand, AdEntity>()
                .ForMember(dest => dest.WebLink, opts => opts.MapFrom(src => src.LinkUrl));
        }
    }
}
