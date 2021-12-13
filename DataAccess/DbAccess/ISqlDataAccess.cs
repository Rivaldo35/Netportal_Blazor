
namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionsStringName);
        Task SaveData<T>(string storedProcedure, T parameters, string connectionsStringName);
    }
}