using Postgrest.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backapitest.Model
{
    public class PrimaryKeyAttributeConverter : JsonConverter<PrimaryKeyAttribute>
    {
        public override PrimaryKeyAttribute Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Implement if needed for deserialization
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, PrimaryKeyAttribute value, JsonSerializerOptions options)
        {
            // Write the primary key attribute value as needed
            writer.WriteStringValue(value.ColumnName); // For example, write the name of the primary key attribute
        }
    }

}
