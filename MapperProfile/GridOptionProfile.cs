using AutoMapper;
using Core.Infrastructure.Grid;
using Core.Infrastructure.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Infrastructure.Grid.KendoGridFilter;

namespace Core.Infrastructure.MapperProfile
{
    public class GridOptionProfile   : Profile
    {
        public GridOptionProfile()
        {
            CreateMap<GridOptions, GridOptionsV2>();
            CreateMap<KendoGridFilter.GridSort, Sort>();
            CreateMap<KendoGridFilter.GridFilters, Filter>();
            CreateMap<KendoGridFilter.GridFilter, Filter>();

            

        }
    }
}
