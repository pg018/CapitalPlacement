using System.Text;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.Services
{
    // this service is static..
    // This is used to perform operations such as adding, updating properties on JsonDocument without explicit object casting
    public static class JsonService
    {
        public static JsonDocument UpdateProperties(JsonDocument originalDocument, string propertiesJson)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions { Indented = true }))
                {
                    writer.WriteStartObject();

                    // Iterate over the properties in the original JSON document
                    foreach (var property in originalDocument.RootElement.EnumerateObject())
                    {
                        // Write the original properties
                        property.WriteTo(writer);
                    }

                    // Deserialize the new properties JSON string to JsonElement
                    JsonDocument newPropertiesDoc = JsonDocument.Parse(propertiesJson);
                    foreach (var newProperty in newPropertiesDoc.RootElement.EnumerateObject())
                    {
                        // If property with the same name exists, overwrite it
                        bool propertyExists = originalDocument.RootElement.TryGetProperty(newProperty.Name, out var existingProperty);
                        if (propertyExists)
                        {
                            writer.WritePropertyName(newProperty.Name);
                            newProperty.Value.WriteTo(writer);
                        }
                        else
                        {
                            // Property doesn't exist, add it
                            newProperty.WriteTo(writer);
                        }
                    }

                    writer.WriteEndObject();
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                // The resulting JSON string will contain the updated properties
                // Convert the written JSON to a JsonDocument and return it
                return JsonDocument.Parse(memoryStream);
            }
        }

        public static JsonDocument AddPropertiesToJsonDocument(JsonDocument originalDocument, string propertyName, string newPropertiesJson)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions { Indented = true }))
                {
                    writer.WriteStartObject();

                    // Iterate over the properties in the original JSON document
                    foreach (var property in originalDocument.RootElement.EnumerateObject())
                    {
                        if (property.Name == propertyName && property.Value.ValueKind == JsonValueKind.Object)
                        {
                            writer.WritePropertyName(property.Name);
                            writer.WriteStartObject();

                            // Write the existing properties from the nested object
                            foreach (var nestedProperty in property.Value.EnumerateObject())
                            {
                                nestedProperty.WriteTo(writer);
                            }

                            // Deserialize the new properties JSON string to JsonElement
                            JsonDocument newPropertiesDoc = JsonDocument.Parse(newPropertiesJson);
                            foreach (var newProperty in newPropertiesDoc.RootElement.EnumerateObject())
                            {
                                // Check if the property already exists in the nested object
                                if (!property.Value.EnumerateObject().Any(p => p.Name == newProperty.Name))
                                {
                                    newProperty.WriteTo(writer);
                                }
                            }

                            writer.WriteEndObject(); // End of nested object
                        }
                        else
                        {
                            property.WriteTo(writer);
                        }
                    }

                    writer.WriteEndObject();
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                // The resulting JSON string will contain the updated properties
                return JsonDocument.Parse(memoryStream);
            }
        }

        // A single property is updated in this function.. The property must be at top level of document
        public static JsonDocument UpdateExistingProperty(JsonDocument document, string propertyName, string propertyValue)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions { Indented = true }))
                {
                    writer.WriteStartObject();

                    // Iterate over the properties in the original JSON document
                    foreach (var property in document.RootElement.EnumerateObject())
                    {
                        if (property.Name == propertyName)
                        {
                            // Write the updated property
                            writer.WritePropertyName(propertyName);
                            JsonDocument newPropertyDoc = JsonDocument.Parse(propertyValue);
                            newPropertyDoc.RootElement.WriteTo(writer);
                        }
                        else
                        {
                            // Write other properties as is
                            property.WriteTo(writer);
                        }
                    }

                    writer.WriteEndObject();
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                // The resulting JSON string will contain the updated properties
                // Convert the written JSON to a JsonDocument and return it
                return JsonDocument.Parse(memoryStream);
            }
        }

        public static string SerializeJsonDocument(JsonDocument jsonDocument)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions { Indented = true }))
                {
                    jsonDocument.WriteTo(writer);
                }

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

    }
}
