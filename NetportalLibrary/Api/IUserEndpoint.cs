using EPSDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddUserToRole(string userId, string rolename);
        Task RemoveUserFromRole(string userId, string rolename);
    }
}