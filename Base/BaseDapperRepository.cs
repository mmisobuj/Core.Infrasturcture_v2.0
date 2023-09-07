using Core.Infrastructure.Common;
using Core.Infrastructure.Extensions;
using Core.Infrastructure.Grid;
using Core.Infrastructure.Models;
using Core.Infrastructure.Query;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using static Dapper.SqlMapper;

namespace Core.Infrastructure.Base
{
    public class BaseDapperRepository : IBaseDapperRepository
    {
        public readonly ILogger _logger;
        private readonly IDbConnection _conn;
        private IDbTransaction _tran;
        int commandTimeOut = 1000 * 60 * 5;
        private IConnectionFactory _conFactory;
        public BaseDapperRepository()
        {

        }
        public BaseDapperRepository(IConnectionFactory conn)
        {

            _conn = conn.GetConnection;
            _conFactory = conn;
        }
        public BaseDapperRepository(ILogger logger, IConnectionFactory conn)
        {
            _logger = logger;
            _conn = conn.GetConnection;
            _conFactory = conn;

        }

        public IDbTransaction BeginTransaction()
        {
            try
            {
                _tran = _conFactory.CreateTransaction();

                //if (_conn.State == ConnectionState.Closed)
                //    _conn.Open();

                return _tran;


            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw ex;
            }
            // _dbContext.Database.BeginTransaction();
        }
        //public void SetTran(IDbTransaction tran)
        //{
        //    _tran = tran;
        //}

        public void Commit()
        {
            try
            {

                //  _conn.BeginTransaction()
                _tran = _conFactory.GetTransaction;

                if (_tran != null)
                {
                    _tran.Commit();
                    //_tran.Dispose();
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw ex;
            }
            //  _conn.Close();
        }
        public void Rollback()
        {
            try
            {
                _tran = _conFactory.GetTransaction;

                if (_tran != null)
                {
                    _tran.Rollback();
                    //_tran.Dispose();
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;
            }
            //_conn.Close();
        }
        public async Task<IEnumerable<T>> Query<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QueryAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: commandTimeOut);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
        
        }
        public async Task<T> QuerySingle<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QuerySingleAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: commandTimeOut);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;


            }
       
        }
        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QuerySingleOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;


            }
         
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
            return default;
        }
        public async Task<IEnumerable<T>> SpQuery<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
          
        }

        public async Task<T> SpQuerySingle<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QuerySingleAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }

        }
        public async Task<int> SpQueryScalar(string query, object? parameters = null)
        {
            try
            {
                return await _conn.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }

            return 0;
        }

        public async Task<T> SpQueryFirst<T>(string query, object? parameters = null)
        {
            try
            {
                return await _conn.QueryFirstAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
      

        }
        public async Task<int> ExecuteSpAsync(string query, object? parameters = null)
        {
            //  ResponseResult result = new();
            try
            {
                _tran = _conFactory.GetTransaction;
                if (_tran != null)
                    return await _conn.ExecuteAsync(query, parameters, _tran, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
                else
                    return await _conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
                //result.EffectedRow = count;
                //result.IsSuccessStatus = true;
                //result.StatusCode = 200;
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;
            }
            
             
        }
        public async Task<int> ExecuteAsync(string query, object? parameters = null)
        {
            ResponseResult result = new();

            try
            {
                _tran = _conFactory.GetTransaction;
                if (_tran != null)
                {
                    return await _conn.ExecuteAsync(query, parameters, _tran, commandTimeout: commandTimeOut);

                }
                else
                {
                    return await _conn.ExecuteAsync(query, parameters, commandTimeout: commandTimeOut);

                }
               
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;
            }
            finally
            {
                if (_tran == null)
                {
                    _conn.Close();
                }
            }
        
        }

        public async Task<int> ExecuteScalerAsync(string query, object? parameters = null)
        {
            int result = 0;

            try
            {
                _tran = _conFactory.GetTransaction;
                if (_tran != null)
                {
                    var obj = await _conn.ExecuteScalarAsync(query, parameters, _tran, commandTimeout: commandTimeOut);
                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out result);
                    }
                }
                else
                {
                    var obj = await _conn.ExecuteScalarAsync(query, parameters, commandTimeout: commandTimeOut);
                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out result);
                    }
                }
                  
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
            finally
            {
                if (_tran == null)
                {
                    _conn.Close();
                }
            }
            return result;
        }
        public async Task<int> ExecuteIdentityAsync(string query, object? parameters = null)
        {
            int result = 0;

            try
            {
                DbSettings settings = _conFactory.GetDbSettings();
                query = query.Trim();
                if (settings.DbServer == DbServer.MySQL)
                {

                    if (!query.EndsWith(";"))
                    {
                        query += ";SELECT LAST_INSERT_ID() ";
                    }
                    else
                    {
                        query += " SELECT LAST_INSERT_ID() ";
                    }
                }
                else if (settings.DbServer == DbServer.MSSQL)
                {
                    query += " SELECT @@IDENTITY ";

                }
                else
                {
                    if (!query.EndsWith(";"))
                    {
                        query += ";SELECT LAST_INSERT_ID() ";
                    }
                    else
                    {
                        query += " SELECT LAST_INSERT_ID() ";
                    }
                }




                _tran = _conFactory.GetTransaction;
                if (_tran != null)
                {

                    var obj = await _conn.ExecuteScalarAsync(query, parameters, _tran);
                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out result);
                    }
                }
                else
                {
                    var obj = await _conn.ExecuteScalarAsync(query, parameters);
                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out result);
                    }
                }

               

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
            finally
            {
                
            }
            return result;
        }

        public async Task<GridEntity<T2>> SpQueryGridData<T2>(GridOptions options, string spName, object? parameters = null)
        {
            try
            {
                //var reader = _conn.QueryMultiple(spName, param: parameters, commandType: CommandType.StoredProcedure);
                //var ProductListOne = (await reader.ReadAsync<T2>()).ToList();
                //var ProductListTwo = await reader.ReadSingleAsync<int>();
                //return new GridEntity<T2> { Items = ProductListOne, TotalCount = ProductListTwo };

                var data = await _conn.QueryAsync<T2>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
                return new GridEntity<T2> { Items = data.ToList(), TotalCount = data.Count() };
                // return data.ToGridDataSource(options);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message + " InnerException: ", ex.InnerException);
                throw;

            }
         
        }

        public async Task<GridEntity<T2>> GridDataSourceAync<T2>(string query, string orderByColumn, GridOptions? options = null, string condition = "")
        {

            return await _conn.PagingData<T2>(query, orderByColumn, options, condition);
        }

        public Task<int> Insert<T>(T obj)
        {
            var command = SQLCommandBuilder.GetInsertCommand(obj);
            return ExecuteAsync(command);
        }

        public Task<int> Insert<T>(T obj, string columns)
        {
            var command = SQLCommandBuilder.GetInsertCommand(obj, columns);
            return ExecuteAsync(command);
        }

        public Task<int> Update<T>(T obj, string columns, string condition)
        {
            var command = SQLCommandBuilder.GetUpdateCommand(obj, columns, condition);
            return ExecuteAsync(command);
        }

        public async Task<GridReader> SpQueryMultiple(string spName, object? parameters = null)
        {
            return await _conn.QueryMultipleAsync(spName, parameters, commandType: CommandType.StoredProcedure);

        }
        //      public Task<T1> GetSpMultipleQuery<T1,T2>(string spName)
        //{
        //        //  return await _conn.QueryFirstAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);

        //          var reader = _conn.QueryMultiple(spName, param: new { CategoryID = 1 }, commandType: CommandType.StoredProcedure);
        //          var ProductListOne = reader.Read<T1>().ToList();
        //          var ProductListTwo = reader.Read<T2>().ToList();
        //	return ProductListOne;
        //      }

        //public async Task<bool> Backup(string backup_path)
        //{
        //  return  await _conFactory.Backup(backup_path);
        //}
        //public async Task<bool> Restore(string restore_path)
        //{
        //    return await _conFactory.Restore(restore_path);

        //}


    }
}
