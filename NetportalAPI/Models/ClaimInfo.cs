using NetportalAPI.Data;
using System.Collections.Generic;

namespace NetportalAPI.Models
{
    public class ClaimInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int InstellingId { get; set; }
        public string Instelling { get; set; }
        public List<UserRol> UserRols { get; set; }

    }
}
