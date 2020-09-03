using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsMappingProfile : Profile
    {
        public UpdateAdStatsMappingProfile()
        {
            CreateMap<UpdateAdStatsCommand, AdStatsEntity>();
        }
    }
}
