using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAd
{
    public class UpdateAdMappingProfile : Profile
    {
        public UpdateAdMappingProfile()
        {
            CreateMap<UpdateAdCommand, AdEntity>()
                .ForMember(dest => dest.WebLink, opts => opts.MapFrom(src => src.LinkUrl));
        }
    }
}
