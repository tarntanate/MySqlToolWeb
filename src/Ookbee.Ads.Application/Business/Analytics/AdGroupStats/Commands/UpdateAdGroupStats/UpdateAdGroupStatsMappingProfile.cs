using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatsMappingProfile : Profile
    {
        public UpdateAdGroupStatsMappingProfile()
        {
            CreateMap<UpdateAdGroupStatsCommand, AdGroupStatsEntity>();
        }
    }
}
