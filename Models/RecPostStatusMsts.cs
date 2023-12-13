using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecPostStatusMsts
    {
        public int? UnitCode { get; set; }
        public string RecCode { get; set; }
        public string RecEventCode { get; set; }
        public string PostAppliedDescription { get; set; }
        public long? QualCode { get; set; }
        public string QualDesc { get; set; }
        public long? QualCode1 { get; set; }
        public string QualDesc1 { get; set; }
        public long? QualCode2 { get; set; }
        public string QualDesc2 { get; set; }
        public string QualExp1 { get; set; }
        public string QualExp2 { get; set; }
        public string FyYear { get; set; }
        public DateTime NotifyDtRecruitment { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime? DateOfTest { get; set; }
        public DateTime? DateOfInterview { get; set; }
        public int? NoOfVacancies { get; set; }
        public string RecStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
