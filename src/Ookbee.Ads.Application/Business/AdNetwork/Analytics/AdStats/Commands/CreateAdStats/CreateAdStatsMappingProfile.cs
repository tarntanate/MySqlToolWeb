using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStats.Commands.CreateAdStats
{
    public class CreateAdStatsMappingProfile : Profile
    {
        public CreateAdStatsMappingProfile()
        {
            CreateMap<CreateAdStatsCommand, AdStatsEntity>();
        }
    }
}
