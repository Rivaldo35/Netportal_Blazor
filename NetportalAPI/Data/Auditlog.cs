using System;
using System.Collections.Generic;

namespace NetportalAPI.Data
{
    public partial class Auditlog
    {
        public int AuditlogId { get; set; }
        public int UserId { get; set; }
        public int InstellingId { get; set; }
        public int ApplicatieId { get; set; }
        public int? RapportageId { get; set; }
        public string Actie { get; set; } = null!;
        public DateTime Datetime { get; set; }
    }
}
