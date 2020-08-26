using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdUnitStatsMappingProfile : Profile
    {
        public UpdateAdUnitStatsMappingProfile()
        {
            CreateMap<UpdateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
