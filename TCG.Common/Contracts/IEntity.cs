using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TCG.Common.Contracts;

public interface IEntity
{
    Guid Id { get; set; }
}