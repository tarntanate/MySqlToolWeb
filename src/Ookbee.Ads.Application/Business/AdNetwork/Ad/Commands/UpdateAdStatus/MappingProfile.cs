using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.UpdateAdStatus
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdStatusCommand, AdEntity>();
        }
    }
}
