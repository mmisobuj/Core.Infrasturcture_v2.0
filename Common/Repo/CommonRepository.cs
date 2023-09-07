
using Core.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Common.Repo
{
    public class CommonLibRepository : Base.BaseRepository<Models.CommonComboVm>, ICommonLibRepository
    {
        public readonly ILogger<Models.CommonComboVm> _logger;
        //IManipulateData _manipulate;


        public CommonLibRepository(ILogger<Models.CommonComboVm> logger, IConnectionFactory connectionFactory) : base(logger, connectionFactory)
        {
            _logger = logger;
            //_manipulate = manipulate;
        }
        public async Task<IEnumerable<CommonComboVm>> GetCommonComboData(string peram)
        {
            // CommonLibDataService dataService = new CommonLibDataService();
            var objPeram = peram.Split(',');
            var condition = "";
            string orderBy = "";
            if (objPeram.Length > 3)
            {
                if (objPeram[3] != "")
                {
                    condition = $"Where {objPeram[3]}={objPeram[4]}";
                }
            }
            orderBy = " Order by " + objPeram[1];
            if (IsValidQueryString(peram))
            {
                string query = string.Format(@"Select {0} as Id, {1} as Text from {2} {3} {4}", objPeram[0], objPeram[1], objPeram[2], condition, orderBy);
                return await Query<CommonComboVm>(query);
            }
            return new List<CommonComboVm>();
        }

        public async Task<IEnumerable<object>> GetStoredProcedureData(string spName, object parameter)
        {
            return await SpQuery<object>(spName, parameter);
        }
        public async Task<object> GetSpGridData(Grid.GridOptions options, string spName, object parameter)
        {
            return await SpQueryGridData<object>(options, spName, parameter);
        }
        private bool IsValidQueryString(string peram)
        {
            var objPeram = peram.Split(',');
            foreach (var item in objPeram)
            {
                if (item.Contains(" "))
                {
                    return false;
                }
                if (item.Contains("'"))
                {
                    return false;
                }
                if (item.Contains("--"))
                {
                    return false;
                }
                if (item.Contains("Delete"))
                {
                    return false;
                }
                if (item.Contains("Update"))
                {
                    return false;
                }
                if (item.Contains("Trancate"))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<int> ExecuteStoredProcedure(string spName, object parameter)
        {
            return await ExecuteSpAsync(spName, parameter);
        }
    }
}
