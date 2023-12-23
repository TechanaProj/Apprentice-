using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USERFORM.CommonFunctions;

using USERFORM.Models;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using USERFORM.ViewModels;

using ModelContext = USERFORM.Models.ModelContext;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace USERFORM.Areas.M1.Controllers
{
    [Area("M1")]
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
        
        public IActionResult AddNewRow(USERF01ViewModel uSERF01ViewModel)
        {
            var commonViewModel = new USERF01ViewModel();

            try
            {
                commonViewModel = uSERF01ViewModel;
                commonViewModel.listAtrmsQualificationDtl = RowPopulate(uSERF01ViewModel);

                commonViewModel.IsAlertBox = false;
                commonViewModel.SelectedAction = "AddNewRowSearch";
                commonViewModel.PostdescriptionLOVBind = dropDownListBindWeb.PostdescriptionLOVBind();
                commonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
                commonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
                TempData["CommonViewModel"] = JsonConvert.SerializeObject(commonViewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }

            return Json(commonViewModel);
        }



        public List<AtrmsQualificationDtl> RowPopulate(USERF01ViewModel uSERF01ViewModel)
        {

            var CommonViewModel = new USERF01ViewModel();

            try
            {
                CommonViewModel = uSERF01ViewModel;



                RecPostAvailableMsts obj = _context.RecPostAvailableMsts.Where(x => x.PostAppliedCode == uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedCode).FirstOrDefault();


                //CommonViewModel.listAtrmsQualificationDtl = (CommonViewModel.listAtrmsQualificationDtl == null) ? new List<AtrmsQualificationDtl>() : CommonViewModel.listAtrmsQualificationDtl;
                CommonViewModel.listAtrmsQualificationDtl = new List<AtrmsQualificationDtl>();
                if (true)
                {
                    if (obj.QualDesc != null)
                    {
                        CommonViewModel.listAtrmsQualificationDtl.Add(new AtrmsQualificationDtl() { Sno = 1, Qualification = obj.QualDesc });

                        if (obj.QualDesc1 != null)
                        {
                            CommonViewModel.listAtrmsQualificationDtl.Add(new AtrmsQualificationDtl() { Sno = 2, Qualification = obj.QualDesc1 });
                        }

                        if (obj.QualDesc2 != null)
                        {
                            CommonViewModel.listAtrmsQualificationDtl.Add(new AtrmsQualificationDtl() { Sno = 3, Qualification = obj.QualDesc2 });
                        }
                    }


                }

            }

            catch (Exception ex)
            {

            }
            return ( CommonViewModel.listAtrmsQualificationDtl);
        }


     
        public IActionResult AddNewRowSearch(USERF01ViewModel uSERF01ViewModel)
        {
            // Ensure that HttpContext.Session is not null before accessing it
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Error");
            }

            USERF01ViewModel commonViewModel = new USERF01ViewModel();

            if (TempData["CommonViewModel"] != null)
            {
               
                commonViewModel = JsonConvert.DeserializeObject<USERF01ViewModel>(TempData["CommonViewModel"].ToString());
                commonViewModel.StateLOV = dropDownListBindWeb.StateLOVBind();
                commonViewModel.DistrictLOV = dropDownListBindWeb.DistrictLOVBind();
            }
         
            return View("Create", commonViewModel);
        }

        // GET: RController/CREATE
        [HttpGet]
        public ActionResult Create(DropDownListBindWeb dropDownListBindWeb)
        {
            var ObjCat = new AtrmsPersonalDtl();
            var Objdoc = new AtrmsDocumentsDtlMain();
            var CommonViewModel = new USERF01ViewModel

            {
                listRecPostAvailableMsts = new List<RecPostAvailableMsts>(),
                PostdescriptionLOVBind = dropDownListBindWeb.PostdescriptionLOVBind(),
                listAtrmsQualificationDtl = new List<AtrmsQualificationDtl>(),
                listAtrmsExperienceDtl = new List<AtrmsExperienceDtl>(),
                listAtrmsDocumentsDtlMain = new List<AtrmsDocumentsDtlMain>(),

            };
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


        // POST: RECFSC02Controller/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] USERF01ViewModel uSERF01ViewModel)
        {

            var CommonViewModel = new USERF01ViewModel();
            try
            {

                // Retrieve session values
                int unit = 3;


                if (uSERF01ViewModel.objAtrmsPersonalDtl != null && uSERF01ViewModel.listAtrmsQualificationDtl.Any())

                {

                    // Generate a unique AtId
                    string AtId = primaryKeyGen.Get_EnrolledATCode_PK(unit);

                    uSERF01ViewModel.objAtrmsPersonalDtl.AtId = AtId;
                    uSERF01ViewModel.objAtrmsPersonalDtl.UnitCode = unit;

                    string RecCodeForPersonal = uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription.Split(",")[1];
                    string PostCodeForPersonal = uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription.Split(",")[0];
                    uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription = _context.RecPostAvailableMsts.FirstOrDefault(x => x.PostAppliedCode == PostCodeForPersonal && x.RecCode == RecCodeForPersonal).PostAppliedDescription;

                    _context.AtrmsPersonalDtl.Add(uSERF01ViewModel.objAtrmsPersonalDtl);


                    await _context.SaveChangesAsync();

                    // Loop through qualifications and add them
                    foreach (var qualification in uSERF01ViewModel.listAtrmsQualificationDtl)
                    {

                        qualification.AtId = AtId;
                        qualification.UnitCode = unit; // Set AtId for qualification
                        qualification.RecCode = qualification.RecCode;
                        qualification.CreatedOn = DateTime.Now;
                        _context.AtrmsQualificationDtl.Add(qualification);
                    }

                    if (uSERF01ViewModel.listAtrmsExperienceDtl != null && uSERF01ViewModel.listAtrmsExperienceDtl.Any())
                    {

                        foreach (var experience in uSERF01ViewModel.listAtrmsExperienceDtl)
                        {

                            experience.AtId = AtId;
                            experience.RecCode = experience.RecCode;
                            experience.CreatedOn = DateTime.Now;


                            _context.AtrmsExperienceDtl.Add(experience);
                        }
                    }

                    if (uSERF01ViewModel.listAtrmsDocumentsDtlMain != null && uSERF01ViewModel.listAtrmsDocumentsDtlMain.Any())
                    {
                        int serialNumber = 1;

                        foreach (AtrmsDocumentsDtlMain attachobj in uSERF01ViewModel.listAtrmsDocumentsDtlMain)
                        {
                            if (attachobj.Uploadfile != null)
                            {
                                if (true)
                                {
                                    AtrmsDocumentsDtlMain NewObj = new AtrmsDocumentsDtlMain()
                                    {

                                        AtId = AtId,
                                        Sno = serialNumber,
                                        Uploadfile = attachobj.Uploadfile,
                                        Name = attachobj.Name,
                                        FileName = uSERF01ViewModel.objAtrmsPersonalDtl.FirstName,
                                        Mimetype = attachobj.Mimetype,
                                        StatusDt = DateTime.Now,
                                        Filesize = attachobj.Filesize,

                                    };
                                    _context.AtrmsDocumentsDtlMain.Add(NewObj);
                                    await _context.SaveChangesAsync();
                                    serialNumber++;


                                }

                            }



                        }

                    }


                    CommonViewModel.Message = "ENROLLED " + AtId;
                    CommonViewModel.Alert = "Create";
                    CommonViewModel.Status = "Create";
                    CommonViewModel.ErrorMessage = "";
                }
                else
                {

                    CommonViewModel.Message = "Invalid Post Details. Try Again!";
                    CommonViewModel.ErrorMessage = "Invalid Post Details. Try Again!";
                    CommonViewModel.Alert = "Warning";
                    CommonViewModel.Status = "Warning";
                }

            }
            catch (Exception ex)
            {
                // Handle the exception and log it
                // commonException.GetCommonExcepton(CommonViewModel, ex);

                CommonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
                CommonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
                return Json(CommonViewModel);
            }

            CommonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
            CommonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(CommonViewModel);
        }


    }
}


