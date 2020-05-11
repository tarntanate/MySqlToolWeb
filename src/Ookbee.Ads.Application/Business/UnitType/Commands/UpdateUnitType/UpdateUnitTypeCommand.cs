using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UnitType.Commands.UpdateUnitType
{
    public class UpdateUnitTypeCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public UpdateUnitTypeCommand()
        {
            Id = string.Empty;
        }

        public UpdateUnitTypeCommand(string id, UpdateUnitTypeCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }
    }
}
