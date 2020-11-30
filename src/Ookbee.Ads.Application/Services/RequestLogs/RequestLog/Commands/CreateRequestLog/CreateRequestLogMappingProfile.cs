using AutoMapper;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;

namespace Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateRequestLog
{
    public class CreateRequestLogMappingProfile : Profile
    {
        public CreateRequestLogMappingProfile()
        {
            CreateMap<CreateRequestLogCommand, RequestLogEntity>();
        }
    }
}
