using WasmNetportal.Models;
using System.Threading.Tasks;

namespace WasmNetportal.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}