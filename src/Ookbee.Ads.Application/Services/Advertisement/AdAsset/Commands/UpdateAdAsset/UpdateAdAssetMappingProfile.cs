using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetMappingProfile : Profile
    {
        public UpdateAdAssetMappingProfile()
        {
            CreateMap<UpdateAdAssetCommand, AdAssetEntity>();
        }
    }
}
