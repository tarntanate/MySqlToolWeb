using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.UpdateAdGroupStats
{
    public class UpdateAdGroupStatusMappingProfile : Profile
    {
        public UpdateAdGroupStatusMappingProfile()
        {
            CreateMap<UpdateAdGroupStatsCommand, AdGroupStatsEntity>();
        }
    }
}
