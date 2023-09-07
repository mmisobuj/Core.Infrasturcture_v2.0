using Core.Infrastructure.Base;
using Core.Infrastructure.Grid;
using Core.Infrastructure.Models;

namespace Core.Infrastructure.Common.Repo
{
    public interface ICommonLibRepository:IBaseDapperRepository
    {
        Task<IEnumerable<CommonComboVm>> GetCommonComboData(string peram);

		//Task<IEnumerable<CommonComboVm>> GetCommonComboData(string peram);

		Task<IEnumerable<dynamic>> GetStoredProcedureData(string spName, object parameter);
		Task<object> GetSpGridData(GridOptions options, string spName, object parameter);
        Task<int> ExecuteStoredProcedure(string spName, object parameter);

    }
}
