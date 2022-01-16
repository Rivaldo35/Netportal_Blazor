using NetportalAPI.Data;

namespace NetportalAPI.Models
{
    public class UserInfo
    {
        public string? applicaties { get; set; }

        public List<UserRol>? rols { get; set; }
    }
}
