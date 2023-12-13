using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecPostAvailableMsts
    {
        public int? UnitCode { get; set; }
        public string RecCode { get; set; }
        public string PostAppliedCode { get; set; }
        public string PostAppliedDescription { get; set; }
        public long QualCode { get; set; }
        public string QualDesc { get; set; }
        public long? QualCode1 { get; set; }
        public string QualDesc1 { get; set; }
        public long? QualCode2 { get; set; }
        public string QualDesc2 { get; set; }
        public string QualExp1 { get; set; }
        public string QualExp2 { get; set; }
        public string RecStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
