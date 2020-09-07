using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Commands.CreateAdAssetStats
{
    public class CreateAdAssetStatsMappingProfile : Profile
    {
        public CreateAdAssetStatsMappingProfile()
        {
            CreateMap<CreateAdAssetStatsCommand, AdStatsEntity>();
        }
    }
}
