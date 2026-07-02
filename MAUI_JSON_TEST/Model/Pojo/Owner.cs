using System.Text.Json.Serialization;

namespace Pojo
{
    public class Owner
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }
    }
}
