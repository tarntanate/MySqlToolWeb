using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.UpdateAdAsset
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdAssetCommand, AdAssetEntity>();
        }
    }
}
