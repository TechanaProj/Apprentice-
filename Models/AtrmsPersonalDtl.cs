using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class AtrmsPersonalDtl
    {
        public long? UnitCode { get; set; }
        public string AtId { get; set; }
        public decimal? RollNo { get; set; }


        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string IdentificationMark { get; set; }
        public string EmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public long? MobileNumber { get; set; }
        public long? AlternateNumber { get; set; }
        public long? AadharNo { get; set; }
        public string MaritalStatus { get; set; }
        public string CAddress { get; set; }
        public string PAddress { get; set; }
        public string HouseNo { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Hometown { get; set; }
        public int? Pincode { get; set; }
        public string Country { get; set; }
        public string Qualification { get; set; }
        public string TotExp { get; set; }
        public string ExApperentice { get; set; }
        public string LandLoser { get; set; }
        public string RegNoEmpExchange { get; set; }
        public string NameEmpExchange { get; set; }
        public string MhrdNats { get; set; }
        public string RegNumberMhrdNats { get; set; }
        public string IffcoEmployee { get; set; }
        public int? RelativeEmpId { get; set; }
        public string RelativeName { get; set; }
        public string RelativeUnit { get; set; }
        public string RelativePost { get; set; }
        public string RelativeRelation { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string RecCode { get; set; }
        public string PostAppliedCode { get; set; }
        public string PostAppliedDescription { get; set; }
        public string RecEventCode { get; set; }
        public string RecEventStatus { get; set; }
        public string Remarks { get; set; }
        public int? AcceptedBy { get; set; }
        public DateTime? AcceptedOn { get; set; }
        public int? RejectedBy { get; set; }
        public DateTime? RejectedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDt { get; set; }
    }
}
