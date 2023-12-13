using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USERFORM.CommonFunctions;
using USERFORM.Models;

using USERFORM.ViewModels;

using ModelContext = USERFORM.Models.ModelContext;




namespace USERFORM.Areas.M1.Controllers
{
    public class USERF01Controller : Controller
    {

        private readonly ModelContext _context;
        private readonly DropDownListBindWeb dropDownListBindWeb = null;
        private readonly ATRMSCommonService aTRMSCommonService;
         private readonly PrimaryKeyGen primaryKeyGen = null;

        public USERF01Controller(ModelContext context)
        {
            _context = context;
            dropDownListBindWeb = new DropDownListBindWeb();
            aTRMSCommonService = new ATRMSCommonService();
            primaryKeyGen = new PrimaryKeyGen();

        }
        public JsonResult ddl1(string StateCd)
        {
            var DistrictLOV = _context.RecDistrictMsts.Where(X => X.StateCd == StateCd).Select(x => new SelectListItem
            {
                Text = string.Concat(x.DisttCd, " - ", x.DisttName),
                Value = x.DisttCd.ToString()
            }).ToList();
            return Json(DistrictLOV);
        }
        public List<SelectListItem> DistrictLOVBindJSON(string StateCd)
        {
            List<SelectListItem> disttCDLOV = new List<SelectListItem>();
            disttCDLOV = dropDownListBindWeb.ListDistrictBind(StateCd);
            return disttCDLOV;
        }

        // GET: RController/CREATE
        [HttpGet]
        public ActionResult Create(DropDownListBindWeb dropDownListBindWeb)
        {
            var ObjCat = new AtrmsPersonalDtl();
            var Objdoc = new AtrmsDocumentsDtlMain();
            var CommonViewModel = new USERF01ViewModel();
            
                //listRecPostAvailableMsts = new List<RecPostAvailableMsts>(),
                //PostdescriptionLOVBind = dropDownListBindWeb.PostdescriptionLOVBind(),
                //listAtrmsQualificationDtl = new List<AtrmsQualificationDtl>(),
                //listAtrmsExperienceDtl = new List<AtrmsExperienceDtl>(),
                //listAtrmsDocumentsDtlMain = new List<AtrmsDocumentsDtlMain>(),

            // The rest of the code remains unchanged.

            CommonViewModel.listAtrmsPersonalDtl = _context.AtrmsPersonalDtl.ToList();
            CommonViewModel.objAtrmsPersonalDtl = ObjCat;
            CommonViewModel.objAtrmsDocumentsDtlMain = Objdoc;
            CommonViewModel.listAtrmsQualificationDtl = new List<AtrmsQualificationDtl>();
            CommonViewModel.listAtrmsExperienceDtl = new List<AtrmsExperienceDtl>();
            CommonViewModel.listRecPostAvailableMsts = new List<RecPostAvailableMsts>();
            CommonViewModel.PostdescriptionLOVBind = dropDownListBindWeb.PostdescriptionLOVBind();
            CommonViewModel.StateLOV = dropDownListBindWeb.StateLOVBind();
            CommonViewModel.DistrictLOV = dropDownListBindWeb.DistrictLOVBind();
            CommonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
            CommonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();

            CommonViewModel.Status = "Create";
            return View("Create", CommonViewModel);

        }



    }
}
