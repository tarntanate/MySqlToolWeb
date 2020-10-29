using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

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
