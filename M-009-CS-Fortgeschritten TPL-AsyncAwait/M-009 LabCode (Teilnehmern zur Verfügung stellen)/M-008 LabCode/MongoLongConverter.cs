using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonSplit;

public class MongoLongConverter : JsonConverter<long>
{
    public override long Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            // Try to read as Int64 first
            if (reader.TryGetInt64(out var int64Value))
                return int64Value;

            // If that fails, get as double and cast to long (handles decimals like 123.0)
            var doubleValue = reader.GetDouble();
            return (long)doubleValue;
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            if (document.RootElement.TryGetProperty("$numberLong", out var value))
                return long.Parse(value.GetString()!);
        }

        throw new JsonException($"Cannot deserialize long from {reader.TokenType}");
    }

    public override void Write(
        Utf8JsonWriter writer,
        long value,
        JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
