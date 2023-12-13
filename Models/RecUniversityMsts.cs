using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecUniversityMsts
    {
        public long UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string StateCd { get; set; }
        public string DisttCd { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreationDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }

        public RecDistrictMsts DisttCdNavigation { get; set; }
        public RecStateMsts StateCdNavigation { get; set; }
    }
}
