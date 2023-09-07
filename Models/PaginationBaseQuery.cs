using Core.Infrastructure.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Models
{
    public class PaginationBaseQuery: BaseQuery
    {
        public GridOptions? Options { get; set; }

    }
}
