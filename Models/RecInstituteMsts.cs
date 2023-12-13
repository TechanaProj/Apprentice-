using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecInstituteMsts
    {
        public long InstituteCd { get; set; }
        public string InstituteName { get; set; }
        public string InstituteType { get; set; }
        public string UniversityId { get; set; }
        public string StateCd { get; set; }
        public string DisttCd { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
