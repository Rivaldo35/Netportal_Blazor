using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        IConfiguration _Configuration { get; }

        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionsStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        Task SaveData<T>(string storedProcedure, T parameters, string connectionsStringName);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionsStringName);
    }
}