using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Extensions
{
    /// <summary>
    /// Custom JSON converter for <see cref="DateTime"/> objects with a specific format (<c>"yyyy-MM-dd HH:mm:ss</c>").
    /// </summary>
    public class LavaDateTimeConverter : JsonConverter<DateTime>
    {
        /// <inheritdoc/>
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is null)
            {
                return DateTime.MinValue;
            }
            if (DateTime.TryParseExact(reader.Value.ToString(), "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Was not able to parse {nameof(DateTime)} (reading JSON type {objectType.Name})");
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}