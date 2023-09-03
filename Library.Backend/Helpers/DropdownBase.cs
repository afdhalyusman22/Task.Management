using System.Text.Json.Serialization;

namespace Library.Backend.Helpers
{
    public class DropdownBase<T>
    {
        [JsonPropertyName("id")]
        public T Id { get; set; }

        [JsonPropertyName("name")]
        public string Label { get; set; }
    }
}
