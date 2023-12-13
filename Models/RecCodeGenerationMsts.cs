using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecCodeGenerationMsts
    {
        public long? UnitCode { get; set; }
        public string RecCode { get; set; }
        public string FyYear { get; set; }
        public DateTime? NotifyDtRecruitment { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? DateOfTest { get; set; }
        public string RecStatus { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
