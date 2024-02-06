using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using USERFORM.Models;
using USERFORM.CommonFunctions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace USERFORM.ViewModels
{
    public class USERF01ViewModel
    {
        public AtrmsPersonalDtl objAtrmsPersonalDtl { get; set; }
        public List<AtrmsPersonalDtl> listAtrmsPersonalDtl { get; set; }

        public AtrmsQualificationDtl objAtrmsQualificationDtl { get; set; }
        public List<AtrmsQualificationDtl> listAtrmsQualificationDtl { get; set; }

        public AtrmsExperienceDtl objAtrmsExperienceDtl { get; set; }

        public List<AtrmsExperienceDtl> listAtrmsExperienceDtl { get; set; }

        public AtrmsDocumentsDtlMain objAtrmsDocumentsDtlMain { get; set; }

        public List<AtrmsDocumentsDtlMain> listAtrmsDocumentsDtlMain { get; set; }

        public List<SelectListItem> StateLOV { get; set; }
        public string StateCd { get; set; }

        public string StateName { get; set; }
        public List<SelectListItem> DistrictLOV { get; set; }
        public string DisttCd { get; set; }
        public string DisttName { get; set; }

        public string AtId { get; set; }

        public string Report { get; set; }

        public List<SelectListItem> HighestqualificationLOVBind { get; set; }

        public string HighestqualificationLOV { get; set; }

        public string POSTLOV { get; set; }

        public string PostAppliedCode { get; set; }

        public string PostAppliedDescription { get; set; }

        public List<SelectListItem> POSTAPPLIEDDESCRIPTION { get; set; }
        //public string PostAppliedCode { get; set; }
        public List<SelectListItem> POSTAPPLIEDCODE { get; set; }

        public List<SelectListItem> POSTAPPLIEDCODERECCODE { get; set; }

        public List<SelectListItem> PostdescriptionLOVBind { get; set; }

        public List<RecPostAvailableMsts> listRecPostAvailableMsts { get; set; }


        public RecPostAvailableMsts objRecPostAvailableMsts { get; set; }

        public RecOtpDetails objRecOtpDetails { get; set; }
        public RecOtpDetails listRecOtpDetails { get; set; }
        public RecCodeGenerationMsts objRecCodeGenerationMsts { get; set; }
        public RecCodeGenerationMsts listRecCodeGenerationMsts { get; set; }


        public string enteredOTP{get;set;}
        public string MobileNumber{get;set;}

        public string QualDesc { get; set; }
       
        public bool IsAlertBox { get; set; }
        public string SelectedAction { get; set; }
        public string AreaName { get; set; }
        public string SelectedMenu { get; set; }
        public string Status { get; set; }
        public string Message { get; internal set; }
    

        public string Alert { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public DateTime? LastDate { get; internal set; }
        public List<RecCategoryMsts> RecCategoryMaster { get; internal set; }
    }
}
