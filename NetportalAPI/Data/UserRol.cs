using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class UserRol
    {
        public UserRol()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int RolId { get; set; }
        public string? Code { get; set; }
        public string? Omschrijving { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
