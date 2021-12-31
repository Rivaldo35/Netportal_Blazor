using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class Applicatie
    {
        public Applicatie()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int ApplicatieId { get; set; }
        public string? Naam { get; set; }
        public string? Code { get; set; }
        public string? Omschrijving { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
