using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.UpdateAdAsset;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlMappingProfile : Profile
    {
        public GenerateUploadUrlMappingProfile()
        {
            CreateMap<AdAssetDto, UpdateAdAssetRequest>();
        }
    }
}
