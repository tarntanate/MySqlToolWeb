using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdUnitStats.Commands.UpdateAdUnitStats
{
    public class UpdateAdGroupStatsMappingProfile : Profile
    {
        public UpdateAdGroupStatsMappingProfile()
        {
            CreateMap<UpdateAdUnitStatsCommand, AdUnitStatsEntity>();
        }
    }
}
