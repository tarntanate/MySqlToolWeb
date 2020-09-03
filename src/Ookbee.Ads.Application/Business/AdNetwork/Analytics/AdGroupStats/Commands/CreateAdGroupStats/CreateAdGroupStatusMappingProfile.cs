using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatusMappingProfile : Profile
    {
        public CreateAdGroupStatusMappingProfile()
        {
            CreateMap<CreateAdGroupStatsCommand, AdGroupStatsEntity>();
        }
    }
}
