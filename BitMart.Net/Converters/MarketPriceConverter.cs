using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitMart.Net.Converters
{
    internal class MarketPriceConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
                return reader.GetDecimal();

            var str = reader.GetString();
            if (string.IsNullOrEmpty(str))
                return null;

            if (str == "market")
                return null;

            return decimal.Parse(str!, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteStringValue("market");
            else
                writer.WriteNumberValue(value.Value);
        }
    }
}
