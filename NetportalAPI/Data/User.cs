using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class User
    {
        public User()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int UserId { get; set; }
        public int InstellingId { get; set; }
        public string Voornaam { get; set; } = null!;
        public string Achternaam { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string InternalUser { get; set; } = null!;
        public int FailedAttempts { get; set; }
        public DateTime PwdExpDate { get; set; }
        public DateTime? PwdChangedDate { get; set; }

        public virtual Instelling Instelling { get; set; } = null!;
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
