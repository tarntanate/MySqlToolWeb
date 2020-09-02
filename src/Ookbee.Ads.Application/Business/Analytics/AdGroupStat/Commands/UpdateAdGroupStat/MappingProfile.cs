using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.UpdateAdGroupStat
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdGroupStatCommand, AdGroupStatsEntity>();
        }
    }
}
