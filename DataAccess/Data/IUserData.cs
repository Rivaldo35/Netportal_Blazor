using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<UserModel> GetUserById(string id);
        Task<IEnumerable<UserModel>> GetUsers();
        Task InsertUser(UserModel user);
        Task UpdateUser(UserModel user);
    }
}