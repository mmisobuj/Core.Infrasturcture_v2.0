using Core.Infrastructure.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Grid
{
    public class GridOptionsV2
    {
        [JsonProperty("skip")]
        public int skip { get; set; }

        [JsonProperty("take")]
        public int take { get; set; }

        [JsonProperty("groupPaging")]
        public bool groupPaging { get; set; }

        [JsonProperty("sort")]
        public List<Sort>? sort { get; set; }

        [JsonProperty("filter")]
        public Filter? filter { get; set; }

        [JsonProperty("group")]
        public List<GroupRequest>? group { get; set; }

        [JsonProperty("aggregate")]
        public List<AggregateRequest>? aggregate { get; set; }
    }
    public class AutoCompFilters
    {
        //public List<AutoCompFilter> filters { get; set; }
        //public string logic { get; set; }

        [JsonProperty("logic")]
        public string? Logic { get; set; }

        [JsonProperty("field")]
        public string? Field { get; set; }

        [JsonProperty("operator")]
        public string? Operator { get; set; }

        [JsonProperty("value")]
        public object? Value { get; set; }

        [JsonProperty("filters")]
        public List<AutoCompFilter>? Filters { get; set; }
    }
    public class AutoCompFilter
    {
        //public string field { get; set; }
        //public bool ignore { get; set; }
        //public string @operator { get; set; }
        //public string value { get; set; }

        [JsonProperty("logic")]
        public string? Logic { get; set; }

        [JsonProperty("field")]
        public string? Field { get; set; }

        [JsonProperty("operator")]
        public string? Operator { get; set; }

        [JsonProperty("value")]
        public object? Value { get; set; }
    }
}
