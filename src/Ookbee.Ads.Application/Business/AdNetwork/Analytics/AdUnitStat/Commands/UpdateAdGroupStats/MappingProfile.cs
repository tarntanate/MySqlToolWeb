using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
