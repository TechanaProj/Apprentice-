using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecQualificationMsts
    {
        public long QualCode { get; set; }
        public string QualDesc { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
