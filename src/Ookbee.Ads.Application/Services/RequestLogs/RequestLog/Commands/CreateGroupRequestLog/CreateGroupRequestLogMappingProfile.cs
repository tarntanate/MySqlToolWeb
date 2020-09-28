using AutoMapper;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog
{
    public class CreateGroupRequestLogMappingProfile : Profile
    {
        public CreateGroupRequestLogMappingProfile()
        {
            CreateMap<CreateGroupRequestLogCommand, GroupRequestLogEntity>();
        }
    }
}
