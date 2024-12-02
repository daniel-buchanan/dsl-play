using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dsl_play.language.Json.Converters;

public class TreeNodeConverter : JsonConverter
{
    private const string PropertyName = "NodeType";
    
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        => throw new NotImplementedException();

    private Dictionary<object, Type> _knownTypes;
    
    private Dictionary<object, Type> GetKnownTypes(Type abstractType)
    {
        if (null == _knownTypes)
        {
            var knownTypes = new Dictionary<object, Type>();
            foreach (var type in Assembly.GetAssembly(abstractType).GetTypes())
            {
                if (type == abstractType || type.IsAbstract || !type.IsSubclassOf(abstractType))
                    continue;

                var typeGetMethod = type.GetProperty(PropertyName).GetGetMethod();
                var typeClassifier = typeGetMethod.Invoke(Activator.CreateInstance(type), new object[0]);
                if (knownTypes.ContainsKey(typeClassifier))
                    throw new ArgumentException("Duplicate Classes claiming type");

                knownTypes[typeClassifier] = type;
            }
            _knownTypes = knownTypes;
        }
        return _knownTypes;
    }
    
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.Null) return null;

        var knownType = GetKnownTypes(objectType);
        var jObject = JObject.Load(reader);
        var typeProperty = objectType.GetProperty(PropertyName);
        var jsonName = typeProperty.GetCustomAttribute<JsonPropertyAttribute>(true).PropertyName;
        var typeKey = jObject[jsonName].ToObject(typeProperty.PropertyType);
        if (!knownType.ContainsKey(typeKey))
        {
            throw new InvalidDataException("Unkown Type");
        }

        var obj = Activator.CreateInstance(knownType[typeKey]);
        serializer.Populate(jObject.CreateReader(), obj);
        return obj;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.GetCustomAttributes(typeof(JsonConverterAttribute), true)
            .Any(x => (x as JsonConverterAttribute).ConverterType == objectType);throw new NotImplementedException();
    }

    public override bool CanWrite => false;
}