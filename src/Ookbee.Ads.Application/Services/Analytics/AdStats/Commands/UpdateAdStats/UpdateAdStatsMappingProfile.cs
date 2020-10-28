using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Commands.UpdateAdStats
{
    public class UpdateAdStatsMappingProfile : Profile
    {
        public UpdateAdStatsMappingProfile()
        {
            CreateMap<UpdateAdStatsCommand, AdStatsEntity>();
        }
    }
}
