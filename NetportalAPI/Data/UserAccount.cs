using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class UserAccount
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public int? ApplicatieId { get; set; }
        public int? RolId { get; set; }
        public string? Status { get; set; }

        public virtual Applicatie? Applicatie { get; set; }
        public virtual UserRol? Rol { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
