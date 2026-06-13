using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DecideAI.Core.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("brand")]
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [BsonElement("model")]
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [BsonElement("cores")]
    [JsonPropertyName("cores")]
    public int Cores { get; set; }

    [BsonElement("threads")]
    [JsonPropertyName("threads")]
    public int Threads { get; set; }

    [BsonElement("baseClockGHz")]
    [JsonPropertyName("baseClockGHz")]
    public double BaseClockGHz { get; set; }

    [BsonElement("tdpW")]
    [JsonPropertyName("tdpW")]
    public int TdpW { get; set; }
}
