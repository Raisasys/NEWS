using Newtonsoft.Json;

namespace Service.Web;

public class AppEnumConverter : JsonConverter<Enum>
{
    /*private readonly JsonConverter<TValue> _valueConverter;
    private readonly Type _valueType;*/


    /*public xEnumConverter(JsonSerializerOptions options)
    {
        // For performance, use the existing converter if available.
        _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

        // Cache the key and value types.
        _valueType = typeof(TValue);
    }*/


    /*public override TValue ReadJson(JsonReader reader, Type objectType, TValue existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return existingValue;
    }

    public override void WriteJson(JsonWriter writer, TValue value, JsonSerializer serializer)
    {
        var enumItemDto = EnumProvider.Get<TValue>(value);
        writer.WriteValue(enumItemDto);
    }*/

    

    public override void WriteJson(JsonWriter writer, Enum value, JsonSerializer serializer)
    {
        var enumType = value.GetType();
        writer.WriteStartObject();
        writer.WritePropertyName("type");
        writer.WriteValue(enumType.Name);
        writer.WritePropertyName("value");
        writer.WriteValue(value);
        writer.WritePropertyName("name");
        writer.WriteValue(Enum.GetName(enumType, value));
        writer.WritePropertyName("image");
        writer.WriteValue(Enum.GetName(enumType, value).ToLower());
        writer.WriteEndObject();
    }

    public override Enum ReadJson(JsonReader reader, Type objectType, Enum existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return 
            Enum.TryParse(objectType, reader.Value.ToString(), out var val) 
                ? (Enum)val
                : default;
        
        return existingValue;
    }
}