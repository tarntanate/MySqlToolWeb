using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog
{
    public class CreateActivityLogMappingProfile : Profile
    {
        public CreateActivityLogMappingProfile()
        {
            CreateMap<CreateActivityLogCommand, ActivityLogEntity>();
        }
    }
}
