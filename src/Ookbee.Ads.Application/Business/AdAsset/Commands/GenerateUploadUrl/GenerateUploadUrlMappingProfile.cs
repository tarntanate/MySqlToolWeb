using AutoMapper;
using Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlMappingProfile : Profile
    {
        public GenerateUploadUrlMappingProfile()
        {
            CreateMap<AdAssetDto, UpdateAdAssetRequest>();
        }
    }
}