using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecPostMsts
    {
        public long? UnitCode { get; set; }
        public string PostAppliedCode { get; set; }
        public string PostAppliedDescription { get; set; }
        public string AppTrade { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
