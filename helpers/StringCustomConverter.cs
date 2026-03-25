using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace apiBukLitoprocess.helpers;

public class StringCustomConverter:JsonConverter<string?>
{

     public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
                

        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt64().ToString();
        }
        if (reader.TokenType == JsonTokenType.String)
        {
            string? value = reader.GetString();
            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dt) ||
                DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.RoundtripKind, out dt))
            {
                return dt.ToString("yyyy-dd-MM", CultureInfo.InvariantCulture);
            }
            return value;
        }
        
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }        
        reader.Skip();
        return null;
    }


    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        if (value != null)
        {
            writer.WriteStringValue(value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }

}
