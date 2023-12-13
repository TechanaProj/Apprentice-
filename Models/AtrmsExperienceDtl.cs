using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class AtrmsExperienceDtl
    {
        public decimal Sno { get; set; }
        public string AtId { get; set; }
        public string RecCode { get; set; }
        public string NameOfOrganization { get; set; }
        public string JobPosition { get; set; }
        public string JobDescription { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string TotalExperience { get; set; }
        public string LastSalaryDrawn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
