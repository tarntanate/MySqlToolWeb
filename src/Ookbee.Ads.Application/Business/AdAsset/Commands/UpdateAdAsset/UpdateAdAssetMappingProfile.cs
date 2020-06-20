using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetMappingProfile : Profile
    {
        public UpdateAdAssetMappingProfile()
        {
            CreateMap<UpdateAdAssetCommand, AdAssetEntity>();
        }
    }
}
