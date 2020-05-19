﻿using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl
{
    public class GetSignedUrlQuery : IRequest<HttpResult<string>>
    {
        public string Id { get; set; }

        public string FileName => ObjectId.GenerateNewId().ToString();
        
        public string FileExtension { get; set; }

        public GetSignedUrlQuery()
        {
            
        }

        public GetSignedUrlQuery(string id, GetSignedUrlQuery request)
        {
            Id = id;
            FileExtension = request.FileExtension;
        }
    }
}
