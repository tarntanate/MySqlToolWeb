using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQuery : IRequest<HttpResult<IEnumerable<PublisherDto>>>
    {
        public int Start { get; set; }
        
        public int Length { get; set; }

        public GetPublisherListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
