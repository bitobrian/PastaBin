using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Blazorbin.Data;

public class PastaBin
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}