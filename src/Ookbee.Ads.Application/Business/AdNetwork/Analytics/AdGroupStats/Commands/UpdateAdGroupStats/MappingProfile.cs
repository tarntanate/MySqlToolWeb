using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.UpdateAdGroupStats
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdGroupStatsCommand, AdGroupStatsEntity>();
        }
    }
}
