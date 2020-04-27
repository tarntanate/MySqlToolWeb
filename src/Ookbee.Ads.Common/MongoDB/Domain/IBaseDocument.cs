using MongoDB.Bson;

namespace Anna.Common.MongoDB.Domain
{
    public interface IBaseDocument
    {
        long Id { get; set; }
    }
}
