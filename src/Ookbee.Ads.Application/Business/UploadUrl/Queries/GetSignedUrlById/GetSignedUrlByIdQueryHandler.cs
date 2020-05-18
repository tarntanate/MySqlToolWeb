﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetSignedUrlById
{
    public class GetSignedUrlByIdQueryHandler : IRequestHandler<GetSignedUrlByIdQuery, HttpResult<string>>
    {
        private AdsMongoRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public GetSignedUrlByIdQueryHandler(AdsMongoRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<string>> Handle(GetSignedUrlByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<string>> GetOnMongo(string id)
        {
            var result = new HttpResult<string>();
            var signedUrl = await UploadUrlMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == id,
                selector: f => f.SignedUrl
            );
            if (signedUrl == null)
                return result.Fail(404, $"UploadUrl '{id}' doesn't exist.");
            return result.Success(signedUrl);
        }
    }
}