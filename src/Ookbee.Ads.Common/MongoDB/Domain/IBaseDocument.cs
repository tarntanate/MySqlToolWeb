using MongoDB.Bson;

namespace Anna.Common.MongoDB.Domain
{
    public interface IBaseDocument
    {
        string Id { get; set; }
    }
}
