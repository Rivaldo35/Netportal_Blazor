using Netportal.Library.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Netportal.Library.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        void LoggOffUser();
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}