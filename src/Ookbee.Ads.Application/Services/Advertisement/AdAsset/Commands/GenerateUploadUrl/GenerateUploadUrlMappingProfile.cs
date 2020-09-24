using AutoMapper;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.UpdateAdAsset;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlMappingProfile : Profile
    {
        public GenerateUploadUrlMappingProfile()
        {
            CreateMap<AdAssetDto, UpdateAdAssetRequest>();
        }
    }
}
