using AutoMapper;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.UpdateAdAsset;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.GenerateUploadUrl
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdAssetDto, UpdateAdAssetRequest>();
        }
    }
}