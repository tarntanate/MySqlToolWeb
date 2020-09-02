using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
