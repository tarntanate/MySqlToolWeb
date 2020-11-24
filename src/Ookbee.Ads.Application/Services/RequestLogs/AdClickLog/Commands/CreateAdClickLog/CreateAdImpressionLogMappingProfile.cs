using AutoMapper;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog
{
    public class CreateAdClickLogMappingProfile : Profile
    {
        public CreateAdClickLogMappingProfile()
        {
            CreateMap<CreateAdClickLogCommand, AdClickLogEntity>();
        }
    }
}
