using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core.Infrastructure.Paging
{
    public class GroupQuery : Sort
    {
        [DataMember(Name = "aggregates")]
        public IEnumerable<Aggregator> Aggregates { get; set; }
    }
}
