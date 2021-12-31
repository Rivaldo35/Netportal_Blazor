using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class ResetPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime? Datetime { get; set; }
    }
}
