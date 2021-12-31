using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class InstellingType
    {
        public InstellingType()
        {
            InstellingProfiles = new HashSet<InstellingProfile>();
        }

        public int TypeId { get; set; }
        public string? Code { get; set; }
        public string? Omschrijving { get; set; }

        public virtual ICollection<InstellingProfile> InstellingProfiles { get; set; }
    }
}
