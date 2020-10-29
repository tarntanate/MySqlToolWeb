using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.CreateAdStats
{
    public class CreateAdStatsMappingProfile : Profile
    {
        public CreateAdStatsMappingProfile()
        {
            CreateMap<CreateAdStatsCommand, AdStatsEntity>();
        }
    }
}
