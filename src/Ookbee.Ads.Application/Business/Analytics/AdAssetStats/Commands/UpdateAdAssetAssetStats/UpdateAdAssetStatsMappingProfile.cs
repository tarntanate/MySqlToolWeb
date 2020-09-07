using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.UpdateAdAssetStats
{
    public class UpdateAdAssetStatsMappingProfile : Profile
    {
        public UpdateAdAssetStatsMappingProfile()
        {
            CreateMap<UpdateAdAssetStatsCommand, AdStatsEntity>();
        }
    }
}
