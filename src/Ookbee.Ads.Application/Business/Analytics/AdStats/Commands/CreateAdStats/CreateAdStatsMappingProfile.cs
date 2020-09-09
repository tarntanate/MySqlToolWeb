using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsMappingProfile : Profile
    {
        public CreateAdStatsMappingProfile()
        {
            CreateMap<CreateAdStatsCommand, AdStatsEntity>();
        }
    }
}
