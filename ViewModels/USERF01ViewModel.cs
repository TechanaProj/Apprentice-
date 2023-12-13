using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

using USERFORM.Models;
using USERFORM.CommonFunctions;

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


        public List<SelectListItem> POSTAPPLIEDDESCRIPTION { get; set; }
        public string PostAppliedCode { get; set; }
        public List<SelectListItem> POSTAPPLIEDCODE { get; set; }

        public List<SelectListItem> POSTAPPLIEDCODERECCODE { get; set; }

        public List<SelectListItem> PostdescriptionLOVBind { get; set; }

        public List<RecPostAvailableMsts> listRecPostAvailableMsts { get; set; }


        public RecPostAvailableMsts objRecPostAvailableMsts { get; set; }
        public string AreaName { get; internal set; }
        public string SelectedMenu { get; internal set; }
        public string Status { get; internal set; }
    }
}
