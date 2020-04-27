using MongoDB.Bson;

namespace Anna.Common.MongoDB.Domain
{
    public abstract class BaseDocument : IBaseDocument
    {
        public long Id { get; set; }
    }
}
