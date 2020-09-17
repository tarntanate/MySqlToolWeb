using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetMappingProfile : Profile
    {
        public UpdateAdAssetMappingProfile()
        {
            CreateMap<UpdateAdAssetCommand, AdAssetEntity>();
        }
    }
}
