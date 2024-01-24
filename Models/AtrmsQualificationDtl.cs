using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class AtrmsQualificationDtl
    {
        public decimal Sno { get; set; }
        public long? UnitCode { get; set; }
        public string AtId { get; set; }
        public string RecCode { get; set; }
        public string Qualification { get; set; }
        public string Subject { get; set; }
        public string NameOfBoard { get; set; }
        public string YearOfPassing { get; set; }
        public string RollNo { get; set; }

        public string MarksObtained { get; set; }
        public string TotalMarks { get; set; }
        public double? Percentage { get; set; }
        
        public DateTime? CreatedOn { get; set; }
    }
}
