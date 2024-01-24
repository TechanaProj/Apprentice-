using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecDistrictMsts
    {
        public RecDistrictMsts()
        {
            RecUniversityMsts = new HashSet<RecUniversityMsts>();
        }

        public string DisttCd { get; set; }
        public string DisttName { get; set; }
        public string StateCd { get; set; }
        public string Status { get; set; }
        public DateTime? CreationDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }

        public RecStateMsts StateCdNavigation { get; set; }
        public ICollection<RecUniversityMsts> RecUniversityMsts { get; set; }
        public string StateName { get; internal set; }
    }
}
