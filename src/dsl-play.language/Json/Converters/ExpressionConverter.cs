using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace dsl_play.language.Json.Converters;

public class ExpressionConverter : JsonConverter<Expression>
{
    public override void WriteJson(JsonWriter writer, Expression value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override Expression ReadJson(JsonReader reader, Type objectType, Expression existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        Console.WriteLine(reader.Value.ToString());
        return null;
    }
}