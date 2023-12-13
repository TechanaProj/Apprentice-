using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecCategoryMsts
    {
        public long? UnitCode { get; set; }
        public string RecCode { get; set; }
        public string CategoryCode { get; set; }
        public string Category { get; set; }
        public string Deceased { get; set; }
        public string Landloser { get; set; }
        public string ExApp { get; set; }
        public decimal? MinAge { get; set; }
        public decimal? MaxAge { get; set; }
        public DateTime? OnDate { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public int? QualifyingMarksGeneralObc { get; set; }
        public int? QualifyingMarksScSt { get; set; }
        public decimal? MinPassedYear { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModificationDt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
