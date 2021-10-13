using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Converters
{
    /// <summary> Converts date typed values in HTTP requests to "MM-dd-yyyy hh:mm:ss" format.
    /// Also converts date types values in HTTP responses to "MM-dd-yyyy" format. </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary> Checks if the passed date is following the "MM-dd-yyyy" format.
        /// If it is, convert it to "MM-dd-yyyy hh:mm:ss" format.
        /// Else, return an Exception.
        /// <param>reader - A reference to the json reader.</param>
        /// <param>typeToConvert - Defines the type of the passed data.</param>
        /// <returns>DateTime - the converted date </returns>
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) {
                if (DateTime.TryParseExact(
                    reader.GetString(),
                    "MM-dd-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, 
                    out DateTime date))
                {
                    return date;
                }
                throw new FormatException("A DateTime property is not in a proper format.");
            }

        /// <summary> Checks if the passed date is following the "MM-dd-yyyy" format.
        /// If it is, convert it to "MM-dd-yyyy hh:mm:ss" format.
        /// Else, return an Exception.
        /// <param>reader - A reference to the json reader.</param>
        /// <param>typeToConvert - Defines the type of the passed data.</param>
        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) {
                if (null == dateTimeValue) {
                    writer.WriteStringValue("");    
                } else {
                    writer.WriteStringValue(dateTimeValue.ToString(
                    "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
                }
            }
    }
}