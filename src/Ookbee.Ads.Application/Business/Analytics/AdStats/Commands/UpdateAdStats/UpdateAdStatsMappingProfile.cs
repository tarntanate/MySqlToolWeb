using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsMappingProfile : Profile
    {
        public UpdateAdStatsMappingProfile()
        {
            CreateMap<UpdateAdStatsCommand, AdStatsEntity>();
        }
    }
}
