using System.Collections.Generic;
using Newtonsoft.Json;
using Core.Infrastructure.Models.Request;

namespace Core.Infrastructure.Models.Request
{
    public class GroupRequest
    {
        [JsonProperty("field")]
        public string? Field { get; set; }

        [JsonProperty("dir")]
        public string? Dir { get; set; }

        [JsonProperty("aggregates")]
        public List<AggregateRequest>? Aggregates { get; set; }
    }
}
