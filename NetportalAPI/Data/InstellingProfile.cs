using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class InstellingProfile
    {
        public int ProfileId { get; set; }
        public int InstellingId { get; set; }
        public int TypeId { get; set; }

        public virtual Instelling Instelling { get; set; } = null!;
        public virtual InstellingType Type { get; set; } = null!;
    }
}
