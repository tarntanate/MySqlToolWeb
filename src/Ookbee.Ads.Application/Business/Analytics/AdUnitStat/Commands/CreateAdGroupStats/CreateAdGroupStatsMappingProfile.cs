using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Commands.CreateAdUnitStats
{
    public class CreateAdUnitStatsMappingProfile : Profile
    {
        public CreateAdUnitStatsMappingProfile()
        {
            CreateMap<CreateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
