using Newtonsoft.Json;

namespace Core.Infrastructure.Models.Request
{
    public class Sort
    {
        [JsonProperty("field")]
        public string? field { get; set; }

        [JsonProperty("dir")]
        public string? dir { get; set; }
    }
}
