
using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.CreateAdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.DeleteAdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.UpdateAdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeById;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeList;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Management.Controllers
{
    [ApiController]
    [Route("api/ad-group-types")]
    public class AdGroupTypesController : ApiController
    {
        [HttpGet]
        public async Task<Response<IEnumerable<AdGroupTypeDto>>> GetList([FromQuery] int start, [FromQuery] int length, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupTypeListQuery(start, length), cancellationToken);

        [HttpGet("{id}")]
        public async Task<Response<AdGroupTypeDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAdGroupTypeByIdQuery(id), cancellationToken);

        [HttpPost]
        public async Task<Response<long>> Create([FromBody] CreateAdGroupTypeRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateAdGroupTypeCommand(request), cancellationToken);

        [HttpPut("{id}")]
        public async Task<Response<bool>> Update([FromRoute] long id, [FromBody] UpdateAdGroupTypeRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateAdGroupTypeCommand(id, request), cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete([FromRoute] long id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAdGroupTypeCommand(id), cancellationToken);
    }
}
