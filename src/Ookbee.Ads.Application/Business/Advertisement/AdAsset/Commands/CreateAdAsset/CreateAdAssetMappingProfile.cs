using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetMappingProfile : Profile
    {
        public CreateAdAssetMappingProfile()
        {
            CreateMap<CreateAdAssetCommand, AdAssetEntity>();
        }
    }
}