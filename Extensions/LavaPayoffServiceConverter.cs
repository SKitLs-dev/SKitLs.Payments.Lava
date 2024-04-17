//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System.Text.RegularExpressions;

//namespace SKitLs.Payments.Lava.Extensions
//{
//    public partial class LavaPayoffServiceConverter : StringEnumConverter
//    {
//        [GeneratedRegex(@"(?<!^)([A-Z])")]
//        private static partial Regex PascalCaseRegex();

//        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            var value = reader.Value;
//            if (reader.TokenType == JsonToken.String && value is not null)
//            {
//                value = PascalCaseRegex().Replace(value.ToString(), "_$1").ToLower(); // Convert to snake_case
//                return Enum.Parse(objectType, value.ToString()!, true);
//            }
//            else
//            {
//                return base.ReadJson(reader, objectType, existingValue, serializer);
//            }
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            string stringValue = value.ToString();
//            stringValue = Regex.Replace(stringValue, @"(\_[a-z])", m => m.ToString().ToUpper().Trim('_')); // Convert to PascalCase
//            writer.WriteValue(stringValue);
//        }
//    }
//}