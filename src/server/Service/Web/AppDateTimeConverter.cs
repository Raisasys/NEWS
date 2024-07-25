

using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Service.Web; 
public class AppDateTimeConverter : DateTimeConverterBase
{
    //const string DatePattern = "MM/dd/yyyy";
    const string DatePattern = "yyyy-MM-dd";
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = reader.Value?.ToString();
        if (value.IsEmpty()) return null;
        if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
           return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
            return DateTimeOffset.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        throw new Exception("Not found correct type");
    }

    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        string result = string.Empty;
        if (value is DateTimeOffset dateTimeOffset)
            result = dateTimeOffset.ToString(DatePattern);
        else
            result = ((DateTime)value).ToString(DatePattern);

        writer.WriteValue(result);
    }
}

public static class DateTimeJavaScript
{
    private static readonly long DatetimeMinTimeTicks =
        (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

    public static long ToJavaScriptMilliseconds(this DateTime dt)
    {
        return (long)((dt.ToUniversalTime().Ticks - DatetimeMinTimeTicks) / 10000);
    }
}

