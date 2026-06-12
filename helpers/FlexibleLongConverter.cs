using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.helpers;

public sealed class FlexibleLongConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetInt64(out var value))
            {
                return value;
            }

            var decimalValue = reader.GetDecimal();
            if (decimal.Truncate(decimalValue) == decimalValue && decimalValue is >= long.MinValue and <= long.MaxValue)
            {
                return (long)decimalValue;
            }
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var longValue))
            {
                return longValue;
            }

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue) &&
                decimal.Truncate(decimalValue) == decimalValue &&
                decimalValue is >= long.MinValue and <= long.MaxValue)
            {
                return (long)decimalValue;
            }
        }

        throw new JsonException("No se pudo convertir el valor JSON a Int64.");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}

public sealed class FlexibleNullableLongConverter : JsonConverter<long?>
{
    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var converter = new FlexibleLongConverter();
        return converter.Read(ref reader, typeof(long), options);
    }

    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteNumberValue(value.Value);
            return;
        }

        writer.WriteNullValue();
    }
}

public sealed class FlexibleNullableDoubleConverter : JsonConverter<double?>
{
    public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetDouble(out var value))
            {
                return value;
            }
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return 0.0;
            }

            if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
            {
                return doubleValue;
            }
        }

        throw new JsonException("No se pudo convertir el valor JSON a Double.");
    }

    public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteNumberValue(value.Value);
            return;
        }

        writer.WriteNullValue();
    }
}