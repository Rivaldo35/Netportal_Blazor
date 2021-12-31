using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class Instelling
    {
        public Instelling()
        {
            InstellingProfiles = new HashSet<InstellingProfile>();
            Users = new HashSet<User>();
        }

        public int InstellingId { get; set; }
        public string? Code { get; set; }
        public string? SwiftCode { get; set; }
        public string? Naam { get; set; }
        public string? Omschrijving { get; set; }
        public string? Adres { get; set; }
        public string? Kkfnr { get; set; }
        public string? Telnr1 { get; set; }
        public string? Telnr2 { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public DateTime? DatumOpgericht { get; set; }
        public DateTime? DatumOpgeheven { get; set; }

        public virtual ICollection<InstellingProfile> InstellingProfiles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
