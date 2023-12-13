using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecEventStatusMsts
    {
        public string RecEventCode { get; set; }
        public string RecEventStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
