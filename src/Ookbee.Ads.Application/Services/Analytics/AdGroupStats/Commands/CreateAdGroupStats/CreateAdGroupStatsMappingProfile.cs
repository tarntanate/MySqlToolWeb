using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsMappingProfile : Profile
    {
        public CreateAdGroupStatsMappingProfile()
        {
            CreateMap<CreateAdGroupStatsCommand, AdGroupStatsEntity>();
        }
    }
}
