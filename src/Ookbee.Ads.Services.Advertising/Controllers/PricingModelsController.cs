using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.PricingModel;
using Ookbee.Ads.Application.Business.PricingModel.Commands.CreatePricingModel;
using Ookbee.Ads.Application.Business.PricingModel.Commands.DeletePricingModel;
using Ookbee.Ads.Application.Business.PricingModel.Commands.UpdatePricingModel;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetByIdPricingModel;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetListPricingModel;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Advertising.Controllers
{
    [ApiController]
    [Route("api/pricing-models")]
    public class PricingModelsController : ApiController
    {
        [HttpGet]
        public async Task<HttpResult<IEnumerable<PricingModelDto>>> GetList([FromQuery]int start, [FromQuery] int length)
            => await Mediator.Send(new GetListPricingModelCommand(start, length));

        [HttpGet("{id}")]
        public async Task<HttpResult<PricingModelDto>> GetById([FromRoute]string id)
            => await Mediator.Send(new GetByIdPricingModelCommand(id));

        [HttpPost]
        public async Task<HttpResult<string>> Create([FromBody]CreatePricingModelCommand request)
            => await Mediator.Send(new CreatePricingModelCommand(request));

        [HttpPut("{id}")]
        public async Task<HttpResult<bool>> Update([FromRoute]string id, [FromBody]UpdatePricingModelCommand request)
            => await Mediator.Send(new UpdatePricingModelCommand(id, request));

        [HttpDelete("{id}")]
        public async Task<HttpResult<bool>> Delete([FromRoute]string id)
            => await Mediator.Send(new DeletePricingModelCommand(id));
    }
}
