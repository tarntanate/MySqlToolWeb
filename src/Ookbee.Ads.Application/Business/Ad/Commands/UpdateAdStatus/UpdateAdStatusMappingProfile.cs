using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusMappingProfile : Profile
    {
        public UpdateAdStatusMappingProfile()
        {
            CreateMap<UpdateAdStatusCommand, AdEntity>();
        }
    }
}
