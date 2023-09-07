using Core.Infrastructure.Grid;
using System.Data;
using static Dapper.SqlMapper;

namespace Core.Infrastructure.Base
{
    public interface IBaseDapperRepository
    {
        IDbTransaction BeginTransaction();

        void Commit();

        void Rollback();
        Task<IEnumerable<T>> Query<T>(string query, object parameters = null);
        Task<T> QuerySingle<T>(string query, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null);
        Task<IEnumerable<T>> SpQuery<T>(string query, object parameters = null);
        Task<T> SpQuerySingle<T>(string query, object parameters = null);
        Task<T> SpQueryFirst<T>(string query, object parameters = null);
        Task<int> ExecuteSpAsync(string query, object parameters = null);
        Task<int> ExecuteScalerAsync(string query, object parameters = null);
        Task<int> ExecuteIdentityAsync(string query, object parameters = null);
        Task<GridEntity<T2>> GridDataSourceAync<T2>(string query, string orderByColumn, GridOptions options = null, string condition = "");
        Task<int> Insert<T>(T obj);
        Task<int> Insert<T>(T obj, string columns);
        Task<int> Update<T>(T obj, string columns, string condition);

        Task<GridReader> SpQueryMultiple(string query, object parameters = null);



        //Task<bool> Backup(string backup_path);
        //Task<bool> Restore(string restore_path);

    }
}
