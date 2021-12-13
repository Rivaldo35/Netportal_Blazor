using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DbAccess
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;


        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }
        public IConfiguration _Configuration { get; }
        public string GetConnectionString(string name)
        {
            return _Configuration.GetConnectionString(name);
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionsStringName)
        {
            string connectionString = GetConnectionString(connectionsStringName);
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                return await cnn.QueryAsync<T>(storedProcedure, parameters,
                     commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionsStringName)
        {
            string connectionString = GetConnectionString(connectionsStringName);
            using IDbConnection cnn = new SqlConnection(connectionString);

            await cnn.ExecuteAsync(storedProcedure, parameters,
                 commandType: CommandType.StoredProcedure);

        }
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction(string connectionsStringName)
        {
            string connectionString = GetConnectionString(connectionsStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            isClosed = false;
        }
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }
        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
               commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
        private bool isClosed = false;

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
        }
        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;

        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispose method");
                }

            }
            _transaction = null;
            _connection = null;
        }

        //Open connect/start transaction method
        //Load using the transaction
        //save using the transaction
        //Close connection/stop transaction method
        //dispose
    }

}

