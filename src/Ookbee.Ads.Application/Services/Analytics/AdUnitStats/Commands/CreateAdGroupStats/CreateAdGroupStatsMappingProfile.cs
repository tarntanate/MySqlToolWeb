using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdGroupStatsMappingProfile : Profile
    {
        public CreateAdGroupStatsMappingProfile()
        {
            CreateMap<CreateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
