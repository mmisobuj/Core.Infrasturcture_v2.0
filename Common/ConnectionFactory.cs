﻿using Core.Infrastructure.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace Core.Infrastructure.Common
{
    public class ConnectionFactory : IConnectionFactory
    {

        //private string _connectionString;
        // AppSettings _appSettings;
        private readonly IDbConnection Connection;
        private IDbTransaction _transaction;

        private DbSettings _DbSetting;
        public ConnectionFactory(AppSettings appSettings)
        {
            if (IsValid())
            {
                DbServer dbServer;
                switch (appSettings.DbServer.ToLower())
                {
                    case "mariadb":
                        dbServer = DbServer.MariaDB;
                        break;
                    case "mysql":
                        dbServer = DbServer.MySQL;
                        break;
                    case "mssql":
                        dbServer = DbServer.MSSQL;
                        break;
                    default:
                        dbServer = DbServer.MySQL;
                        break;
                }
                _DbSetting = new DbSettings
                {
                    DbServer = dbServer,
                    ConnectionString = appSettings.ConnectionString
                };

                if (_DbSetting.DbServer == DbServer.MariaDB)
                {
                    if (Connection == null)
                        Connection = new MySqlConnection(_DbSetting.ConnectionString);
                }
                else if (_DbSetting.DbServer == DbServer.MySQL)
                {
                    if (Connection == null)
                        Connection = new MySqlConnection(_DbSetting.ConnectionString);
                }

                else if (_DbSetting.DbServer == DbServer.MSSQL)
                {
                    if (Connection == null)

                        Connection = new SqlConnection(_DbSetting.ConnectionString);

                }
                else
                {
                    if (Connection == null)
                        Connection = new SqlConnection(_DbSetting.ConnectionString);
                }

                //  return Connection;
            }

        }
        public ConnectionFactory(DbSettings dbSettings)
        {
            if (IsValid())
            {
                _DbSetting = dbSettings;

                if (_DbSetting.DbServer == DbServer.MariaDB)
                {
                    if (Connection == null)
                        Connection = new MySqlConnection(_DbSetting.ConnectionString);
                }
                else if (_DbSetting.DbServer == DbServer.MySQL)
                {
                    if (Connection == null)
                        Connection = new MySqlConnection(_DbSetting.ConnectionString);
                }

                else if (_DbSetting.DbServer == DbServer.MSSQL)
                {
                    if (Connection == null)
                        Connection = new SqlConnection(_DbSetting.ConnectionString);

                }
                else
                {
                    if (Connection == null)
                        Connection = new SqlConnection(_DbSetting.ConnectionString);
                }
            }
        }
        public IDbTransaction CreateTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            _transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);

            return _transaction;
        }

        public IDbConnection GetConnection => Connection;
        public IDbTransaction GetTransaction => _transaction;

        //public string GetConnectionString()
        //{
        //    return _connectionString;
        //}

        private bool IsValid()
        {
            if (DateTime.Now < new DateTime(2024, 6, 30))
            {
                return true;
            }
            return false;
        }

        public DbSettings GetDbSettings()
        {
            return _DbSetting;
        }

        //public async Task<bool> Backup(string backup_path)
        //{


        //    // string constring = "server=localhost;user=root;pwd=qwerty;database=test;";
        //    string file = backup_path;
        //    using (MySqlConnection conn = new MySqlConnection(_DbSetting.ConnectionString))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            using (MySqlBackup mb = new MySqlBackup(cmd))
        //            {
        //                cmd.Connection = conn;
        //                conn.Open();
        //                mb.ExportToFile(file);
        //                conn.Close();
        //                return true;
        //            }
        //        }
        //    }
        //}
        //public async Task<bool> Restore(string restore_path)
        //{
        //    // string constring = "server=localhost;user=root;pwd=qwerty;database=test;";
        //    string file = restore_path;
        //    using (MySqlConnection conn = new MySqlConnection(_DbSetting.ConnectionString))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            using (MySqlBackup mb = new MySqlBackup(cmd))
        //            {
        //                cmd.Connection = conn;
        //                conn.Open();
        //                mb.ImportFromFile(file);
        //                conn.Close();
        //                return true;
        //            }
        //        }
        //    }
        //}
    }
}
