using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecStateMsts
    {
        public RecStateMsts()
        {
            RecDistrictMsts = new HashSet<RecDistrictMsts>();
            RecUniversityMsts = new HashSet<RecUniversityMsts>();
        }

        public string stateCode { get; set; }
        public string StateName { get; set; }
        public string Status { get; set; }
        public DateTime? CreationDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }

        public ICollection<RecDistrictMsts> RecDistrictMsts { get; set; }
        public ICollection<RecUniversityMsts> RecUniversityMsts { get; set; }
        public string StateCd { get; internal set; }
    }
}
