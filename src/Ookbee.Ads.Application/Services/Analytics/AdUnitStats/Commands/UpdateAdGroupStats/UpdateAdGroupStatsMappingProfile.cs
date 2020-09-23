using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdGroupStatsMappingProfile : Profile
    {
        public UpdateAdGroupStatsMappingProfile()
        {
            CreateMap<UpdateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
