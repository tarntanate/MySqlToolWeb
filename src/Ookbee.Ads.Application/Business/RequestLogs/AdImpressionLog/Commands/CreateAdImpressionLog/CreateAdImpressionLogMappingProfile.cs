using AutoMapper;
using Ookbee.Ads.Domain.Entities.RequestLogEntities;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog
{
    public class CreateAdImpressionLogMappingProfile : Profile
    {
        public CreateAdImpressionLogMappingProfile()
        {
            CreateMap<CreateAdImpressionLogCommand, AdImpressionLogEntity>();
        }
    }
}
