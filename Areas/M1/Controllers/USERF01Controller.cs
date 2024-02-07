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
using IFFCO.Web;
using System.Net.Http;
using System.Text;
using System.Net;
using System.IO;
using Devart.Data.Oracle;
using System.Data;

namespace USERFORM.Areas.M1.Controllers
{
    [Area("M1")]
    public class USERF01Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly DropDownListBindWeb dropDownListBindWeb = null;
        private readonly ATRMSCommonService aTRMSCommonService;
        private readonly PrimaryKeyGen primaryKeyGen = null;


        //private object _httpClient;

        // public bool SomeCondition { get; private set; }

        public USERF01Controller(ModelContext context)
        {
            _context = context;
            dropDownListBindWeb = new DropDownListBindWeb();
            aTRMSCommonService = new ATRMSCommonService();
            primaryKeyGen = new PrimaryKeyGen();

        }

        public IActionResult Index(string AtId)
        {
            try
            {
                USERF01ViewModel obj = new USERF01ViewModel();


                obj = _context.AtrmsPersonalDtl
                    .Where(x => x.AtId == AtId)
                    .Select(x => new USERF01ViewModel
                    {
                        AtId = x.AtId,
                        // Populate other properties as needed based on your model

                        // Add more properties here...
                        listAtrmsQualificationDtl = _context.AtrmsQualificationDtl
                            .Where(q => q.AtId == AtId)
                            .ToList()
                    })
                    .FirstOrDefault();

                if (obj == null)
                {
                    // Handle the case where no data is found for the given AtId
                    return RedirectToAction("Error");
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Handle the exception as needed
                return RedirectToAction("Error");
            }
        }





        public JsonResult ddl1(string StateCd)
        {
            var DistrictLOV = _context.RecDistrictMsts.Where(X => X.StateCd == StateCd).Select(x => new SelectListItem
            {
                Text = (x.DisttName),
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


        //public JsonResult ddl2(string QualCode)
        //{
        //    var POSTLOV = _context.RecPostAvailableMsts.Where(X => X.QualCode.ToString() == QualCode).Select(x => new SelectListItem
        //    {
        //        Text = string.Concat(x.PostAppliedCode, " - ", x.PostAppliedDescription),
        //        Value = x.PostAppliedCode.ToString() + "," + x.RecCode
        //    }).ToList();
        //    return Json(POSTLOV);
        //}
        public JsonResult ddl2(string QualCode)
        {
            var POSTLOV = _context.RecPostAvailableMsts.Where(X => X.QualCode.ToString() == QualCode).Select(x => new SelectListItem
            {
                Text = (x.PostAppliedDescription),
                Value = x.PostAppliedCode.ToString() + "," + x.RecCode
            }).ToList();
            return Json(POSTLOV);
        }

        public List<SelectListItem> POSTLOVBinddJSON(string QualCode)
        {
            List<SelectListItem> POSTLOV = new List<SelectListItem>();
            POSTLOV = dropDownListBindWeb.ListPOSTBind(QualCode);
            return POSTLOV;
        }


        public IActionResult AddNewRow(USERF01ViewModel uSERF01ViewModel)
        {
            var obj = new USERF01ViewModel();

            try
            {
                obj = uSERF01ViewModel;
                obj.listAtrmsQualificationDtl = RowPopulate(uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedCode);

                //    commonViewModel.IsAlertBox = false;
                //    commonViewModel.SelectedAction = "AddNewRowSearch";
                //    commonViewModel.PostdescriptionLOVBind = dropDownListBindWeb.PostdescriptionLOVBind();
                //    commonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
                //    commonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
                //    TempData["CommonViewModel"] = JsonConvert.SerializeObject(commonViewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }

            return Json(obj);
        }



        public List<AtrmsQualificationDtl> RowPopulate(string PostAppliedCode)
        {

            var CommonViewModel = new USERF01ViewModel();

            try
            {
                //CommonViewModel = uSERF01ViewModel;



                RecPostAvailableMsts obj = _context.RecPostAvailableMsts.Where(x => x.PostAppliedCode == PostAppliedCode).FirstOrDefault();


                CommonViewModel.listAtrmsQualificationDtl = (CommonViewModel.listAtrmsQualificationDtl == null) ? new List<AtrmsQualificationDtl>() : CommonViewModel.listAtrmsQualificationDtl;
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
            return (CommonViewModel.listAtrmsQualificationDtl);
        }

        [HttpPost]
        public IActionResult CalculatePercentage(int marksObtained, int totalMarks, string qualification, string category)
        {
            try
            {
                if (marksObtained < 0 || totalMarks <= marksObtained)
                {
                    return Json(new { success = false, message = "Invalid input values" });
                }

                // Ensure floating-point division
                var percentage = ((double)marksObtained / totalMarks) * 100;

                var reccode = _context.RecCodeGenerationMsts.Where(x => x.RecStatus == "A").Select(x => x.RecCode).FirstOrDefault();

                int? TestObj = 0;

                if (qualification == "BSC(PCM)")
                {
                    TestObj = _context.RecCategoryMsts.Where(x => x.RecCode == reccode && x.Category == category).Select(x => x.QualifyingMarks1).FirstOrDefault();

                }
                else if (qualification == "BSC(PCM/PCB)")
                {
                    TestObj = _context.RecCategoryMsts.Where(x => x.RecCode == reccode && x.Category == category).Select(x => x.QualifyingMarks2).FirstOrDefault();
                }
                else if (qualification == "ITI" || qualification == "DIPLOMA")
                {
                    TestObj = _context.RecCategoryMsts.Where(x => x.RecCode == reccode && x.Category == category).Select(x => x.QualifyingMarks3).FirstOrDefault();
                }

                if (TestObj.HasValue && TestObj <= percentage)
                {
                    return Json(new { success = true, message = "Success: Percentage is greater than TestObj", percentage = percentage });
                }
                else
                {
                    return Json(new { success = false, message = "Percentage is not equal to Required %", percentage = percentage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred", error = ex.Message });
            }
        }


        //public IActionResult UpdateDateRange(string category, string LandLoser)
        //{
        //    var recCode = _context.RecCodeGenerationMsts.Where(x => x.RecStatus == "A").Select(x => x.RecCode).FirstOrDefault();

        //    var obj = new RecCategoryMsts();

        //    if (LandLoser == "YES")
        //    {
        //        LandLoser = "Y";
        //        obj = _context.RecCategoryMsts.SingleOrDefault(x => x.RecCode == recCode && x.Category == category && x.Deceased == "Y" && x.Landloser == LandLoser && x.ExApp == "N");
        //    }
        //    else if (LandLoser == "NO")
        //    {
        //        LandLoser = "N";
        //        obj = _context.RecCategoryMsts.SingleOrDefault(x => x.RecCode == recCode && x.Category == category && x.Deceased == "N" && x.Landloser == LandLoser && x.ExApp == "N");
        //    }

        //    // Update date range based on obj, if needed

        //    return Json(new { success = true, message = "Success: ", dateOfBirthData = obj });
        //}


        //public IActionResult agematching(string category, string LandLoser,string dob)
        //{
        //    var testdob = dob;

        //    var recCode = _context.RecCodeGenerationMsts.Where(x => x.RecStatus == "A").Select(x => x.RecCode).FirstOrDefault();

        //    var obj = new RecCategoryMsts();

        //    if (LandLoser == "YES")
        //    {
        //        LandLoser = "Y";
        //       obj = _context.RecCategoryMsts.SingleOrDefault(x => x.RecCode == recCode && x.Category == category && x.Deceased == "Y" && x.Landloser == LandLoser && x.ExApp == "N");
        //    }
        //    else if (LandLoser == "NO")
        //    {
        //        LandLoser = "N";
        //        obj = _context.RecCategoryMsts.SingleOrDefault(x => x.RecCode == recCode && x.Category == category && x.Deceased == "N" && x.Landloser == LandLoser && x.ExApp == "N");
        //    }

        //    // Update date range based on obj, if needed

        //    return Json(new { success = true, message = "Success: ", dateOfBirthData=obj });
        //}





        // GET: RController/CREATE
        [HttpGet]
        public ActionResult Create(DropDownListBindWeb dropDownListBindWeb)
        {
            var ObjCat = new AtrmsPersonalDtl();
            var Objdoc = new AtrmsDocumentsDtlMain();

            // Assuming you have a context for the REC_CODE_GEN table
            var recCodeGen = _context.RecCodeGenerationMsts.Where(x => x.RecStatus == "A").Select(x => x.LastDate).FirstOrDefault(); // Adjust the query as needed

            var CommonViewModel = new USERF01ViewModel
            {
                listRecPostAvailableMsts = new List<RecPostAvailableMsts>(),
                HighestqualificationLOVBind = dropDownListBindWeb.HighestqualificationLOVBind(),
                listAtrmsQualificationDtl = new List<AtrmsQualificationDtl>(),
                listAtrmsExperienceDtl = new List<AtrmsExperienceDtl>(),
                listAtrmsDocumentsDtlMain = new List<AtrmsDocumentsDtlMain>(),
                listAtrmsPersonalDtl = _context.AtrmsPersonalDtl.ToList(),
                objAtrmsPersonalDtl = ObjCat,
                objAtrmsDocumentsDtlMain = Objdoc,

                StateLOV = dropDownListBindWeb.StateLOVBind(),
                DistrictLOV = dropDownListBindWeb.DistrictLOVBind(),
                AreaName = this.ControllerContext.RouteData.Values["area"].ToString(),
                SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString(),
                Status = "Create"
            };


            if (recCodeGen != null && recCodeGen <= DateTime.Now)

            {

                return View("DateOver", CommonViewModel);

            }
            else
            {


                return View("Create", CommonViewModel);
            }



            //return View("Create", CommonViewModel);
        }


        // POST: RECFSC02Controller/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] USERF01ViewModel uSERF01ViewModel)
        {

            var CommonViewModel = new USERF01ViewModel();
            try
            {

                int unit = 3;


                //var stateCode = uSERF01ViewModel.objAtrmsPersonalDtl.State;

                //var stateName = _context.RecStateMsts.Where(x => x.stateCode == x.stateCode).Select(x => x.StateName).FirstOrDefault();
                var stateCode = uSERF01ViewModel.objAtrmsPersonalDtl.State.Trim(); // Trim any leading or trailing whitespaces

                var stateName = _context.RecStateMsts
                    .Where(x => x.stateCode.Trim().Equals(stateCode, StringComparison.OrdinalIgnoreCase)) // Case-insensitive and trimmed comparison
                    .Select(x => x.StateName)
                    .FirstOrDefault();

                var disstCode = uSERF01ViewModel.objAtrmsPersonalDtl.District.Trim();

                var disstName = _context.RecDistrictMsts
                    .Where(x => x.DisttCd.Trim().Equals(disstCode, StringComparison.OrdinalIgnoreCase)) // Case-insensitive and trimmed comparison
                    .Select(x => x.DisttName)
                    .FirstOrDefault();

                var someQualCode = uSERF01ViewModel.objAtrmsPersonalDtl.Qualification.Trim();

                var qualificationName = _context.RecQualificationMsts
                    .Where(x => x.QualCode.Trim().Equals(someQualCode, StringComparison.OrdinalIgnoreCase))
                    .Select(x => x.QualDesc)
                    .FirstOrDefault();


                var somePostAppliedCodes = uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription?.Trim().Split(',');

                var postAppDescription = _context.RecPostAvailableMsts
               .Where(x => somePostAppliedCodes.Contains(x.PostAppliedCode.Trim(), StringComparer.OrdinalIgnoreCase) &&
                           somePostAppliedCodes.Contains(x.RecCode.Trim(), StringComparer.OrdinalIgnoreCase))
               .Select(x => x.PostAppliedDescription)
               .FirstOrDefault();

                // Assign qualificationName to the Qualification property
                uSERF01ViewModel.objAtrmsPersonalDtl.Qualification = qualificationName;
                var existingRecord = _context.AtrmsPersonalDtl.FirstOrDefault(x => x.AadharNo == uSERF01ViewModel.objAtrmsPersonalDtl.AadharNo && x.Qualification == qualificationName);



                if (existingRecord != null)
                {
                    CommonViewModel.Message = "Aadhar number and qualification already exist. Please provide unique values.";
                    CommonViewModel.ErrorMessage = "Aadhar number and qualification already exist. Please provide unique values.";
                    CommonViewModel.Alert = "Warning";
                    CommonViewModel.Status = "Warning";
                    return Json(CommonViewModel);
                }



                if (uSERF01ViewModel.objAtrmsPersonalDtl != null && uSERF01ViewModel.listAtrmsQualificationDtl.Any())
                {

                    // Generate a unique AtId
                    string AtId = primaryKeyGen.Get_EnrolledATCode_PK(unit);


                    uSERF01ViewModel.objAtrmsPersonalDtl.AtId = AtId;
                    uSERF01ViewModel.objAtrmsPersonalDtl.UnitCode = unit;


                    uSERF01ViewModel.objAtrmsPersonalDtl.State = stateName;
                    uSERF01ViewModel.objAtrmsPersonalDtl.District = disstName;
                    uSERF01ViewModel.objAtrmsPersonalDtl.Qualification = qualificationName;
                    uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription = postAppDescription;



                    //string RecCodeForPersonal = uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription.Split(",")[1];
                    //string PostCodeForPersonal = uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription.Split(",")[0];
                    //uSERF01ViewModel.objAtrmsPersonalDtl.PostAppliedDescription = _context.RecPostAvailableMsts.FirstOrDefault(x => x.PostAppliedCode == PostCodeForPersonal && x.RecCode == RecCodeForPersonal).PostAppliedDescription;

                    _context.AtrmsPersonalDtl.Add(uSERF01ViewModel.objAtrmsPersonalDtl);


                    foreach (var qualification in uSERF01ViewModel.listAtrmsQualificationDtl)
                    {

                        qualification.AtId = AtId;
                        qualification.RecCode = uSERF01ViewModel.objAtrmsPersonalDtl.RecCode;
                        qualification.UnitCode = unit; // Set AtId for qualification
                        qualification.RecCode = qualification.RecCode;
                        qualification.CreatedOn = DateTime.Now;
                        _context.AtrmsQualificationDtl.Add(qualification);
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
                                        RecCode = uSERF01ViewModel.objAtrmsPersonalDtl.RecCode,
                                        Sno = serialNumber,
                                        Uploadfile = attachobj.Uploadfile,
                                        Name = attachobj.Name,
                                        FileName = uSERF01ViewModel.objAtrmsPersonalDtl.FirstName,
                                        Mimetype = attachobj.Mimetype,
                                        StatusDt = DateTime.Now,
                                        Filesize = attachobj.Filesize,

                                    };
                                    _context.AtrmsDocumentsDtlMain.Add(NewObj);

                                    serialNumber++;

                                    await _context.SaveChangesAsync();
                                }

                            }



                        }

                    }


                    if (AtId != null)
                    {
                        // Retrieve the current serial number from the database or some other persistent storage
                        var currentSerialNumber = _context.AtrmsReportLogGen
                                                            .OrderByDescending(x => x.CreatedDateTime)
                                                            .Select(x => x.Sno)
                                                            .FirstOrDefault();

                        // If no serial number is found, start from 1
                        if (currentSerialNumber == null)
                        {
                            currentSerialNumber = "1";
                        }
                        else
                        {
                            // Increment the serial number for the next entry
                            currentSerialNumber = (int.Parse(currentSerialNumber) + 1).ToString();
                        }

                        string QueryString = string.Empty;
                        var Reportname = "APPLICANT_FORM.aspx";
                        QueryString = "AT_ID=" + AtId;
                        var AreaName = "RECRUITER";
                        GenerateRdlcReportLink ReportObj = new GenerateRdlcReportLink();
                        var report = ReportObj.GenerateLink(Reportname, QueryString, AreaName);
                        CommonViewModel.Report = report;

                        // Create an instance with the current serial number
                        var urlDetails = new AtrmsReportLogGen()
                        {
                            Sno = currentSerialNumber, // Use the incremented serialNumber directly
                            AtId = AtId,
                            RdlcReportLink = report,
                            CreatedDateTime = DateTime.Now,
                        };

                        // Add the instance to the context
                        _context.AtrmsReportLogGen.Add(urlDetails);
                        _context.SaveChanges();
                    }



                    CommonViewModel.AtId = AtId;
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
                //commonException.GetCommonExcepton(CommonViewModel, ex);

                CommonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
                CommonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
                return Json(CommonViewModel);
            }

            CommonViewModel.AreaName = this.ControllerContext.RouteData.Values["area"].ToString();
            CommonViewModel.SelectedMenu = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(CommonViewModel);
        }



        //[HttpPost]
        //public async Task<IActionResult> sendOTP(string MobileNumber, string EmailId)
        //  {
        //    USERF01ViewModel uSERF01ViewModel = new USERF01ViewModel();

        //    if (!string.IsNullOrEmpty(MobileNumber) || !string.IsNullOrEmpty(EmailId))
        //    {
        //        // Validate if the MobileNumber has exactly 10 digits
        //        if (MobileNumber.Length == 10 && MobileNumber.All(char.IsDigit))
        //        {
        //            // Validate if the provided email is valid
        //            if (IsValidEmail(EmailId))
        //            {
        //                // Check if the provided mobile number is associated with the given email
        //                bool mobileAndEmailMatch = _context.RecAtmobilepaMsts.Any(m => m.MobileNo.ToString() == MobileNumber && m.EmailId == EmailId);



        //                if (mobileAndEmailMatch)
        //                {
        //                    return BadRequest(new { Message = "Mobile number and email do not match." });
        //                }

        //                // Check the number of OTPs sent for the given mobile number in the last 24 hours
        //                int maxAttemptsPerDay = 3;
        //                DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

        //                int sentOtpsCount = _context.RecOtpDetails
        //                    .Count(o => o.Mobileemail == MobileNumber && o.Otpdate >= twentyFourHoursAgo);

        //                if (sentOtpsCount >= maxAttemptsPerDay)
        //                {
        //                    return BadRequest(new { Message = "Exceeded OTP sending limit for the day. Only 3 SMS allowed." });
        //                }

        //                int newSerialNo = GenerateUniqueSerialNumber();
        //                string otp = GenerateOTP();
        //                string smsContents = $"Your One-Time Password is {otp}, valid for 30 minutes only. Please do not share your OTP.";

        //                // Create an instance without Smsapiresponse initially
        //                var otpDetail = new RecOtpDetails()
        //                {
        //                    Sno = newSerialNo,
        //                    Mobileemail = MobileNumber,
        //                    Otp = otp,
        //                    Otpdate = DateTime.Now,
        //                    Statuscode = EmailId,
        //                };

        //                // Add the instance to the context without Smsapiresponse
        //                _context.RecOtpDetails.Add(otpDetail);
        //                _context.SaveChanges();

        //                string URL = "http://hindit.biz/api/pushsms?user=Iffco&authkey=92QZpEbhUypU6&sender=IFFCOA&mobile=" + otpDetail.Mobileemail + "&text=" + otpDetail.Otp + "%20is%20the%20verification%20code%20generated%20for%20IFFCO%27s%20Recruitment%20Portal%20.%20Do%20not%20share%20this%20with%20anyone.%20IFFCO%20Aonla%20Unit&unicode=0&rpt=1&summary=1&output=json&entityid=1001232689878836835&templateid=1007642358221818606";

        //                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
        //                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //                StreamReader sr = new StreamReader(resp.GetResponseStream());
        //                string results = sr.ReadToEnd();
        //                sr.Close();

        //                // Update the instance with Smsapiresponse and save changes
        //                otpDetail.Smsapiresponse = results;
        //                _context.SaveChanges();


        //                string result = "X";
        //                result = aTRMSCommonService.sendEmailtouser();


        //                return Ok(new { Message = "OTP sent successfully." });
        //            }
        //            else
        //            {
        //                return BadRequest(new { Message = "Invalid email format." });
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Mobile number should have 10 digits." });
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest(new { Message = "Failed to send OTP." });
        //    }
        //}

        //private bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> sendOTP(string MobileNumber, string EmailId)
        {
            USERF01ViewModel uSERF01ViewModel = new USERF01ViewModel();

            if (string.IsNullOrEmpty(EmailId))
            {
                return BadRequest(new { Message = "Please Enter Email Id" });
            }
            else if (!string.IsNullOrEmpty(MobileNumber) || !string.IsNullOrEmpty(EmailId))
            {
                // Validate if the MobileNumber has exactly 10 digits
                if (MobileNumber.Length == 10 && MobileNumber.All(char.IsDigit))
                {
                    bool isEmailExists = _context.RecAtmobilepaMsts.Any(user => user.EmailId == EmailId);
                    bool isPhoneNumberExists = _context.RecAtmobilepaMsts.Any(user => user.MobileNo.ToString() == MobileNumber);

                    if (!isEmailExists)
                    {
                        if (!isPhoneNumberExists)
                        {
                            return BadRequest(new { Message = "Your Details Not Found. Please Connect with Admin Department." });
                        }
                        else
                        {
                            // Check the number of OTPs sent for the given mobile number in the last 24 hours
                            int maxAttemptsPerDay = 3;
                            DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

                            int sentOtpsCount = _context.RecOtpDetails
                                .Count(o => o.Mobileemail == MobileNumber && o.Otpdate >= twentyFourHoursAgo);

                            if (sentOtpsCount >= maxAttemptsPerDay)
                            {
                                return BadRequest(new { Message = "Exceeded OTP sending limit for the day. Only 3 SMS allowed." });
                            }

                            try
                            {
                                // Send OTP to mobile number
                                // Send OTP to EmailId
                                int newSerialNo = GenerateUniqueSerialNumber();
                                string otp = GenerateOTP();

                                var otpDetail = new RecOtpDetails()
                                {
                                    Sno = newSerialNo,
                                    Mobileemail = MobileNumber,
                                    Otp = otp,
                                    Otpdate = DateTime.Now,
                                    Statuscode = EmailId,
                                };

                                _context.RecOtpDetails.Add(otpDetail);
                                _context.SaveChanges();

                                string URL = "http://hindit.biz/api/pushsms?user=Iffco&authkey=92QZpEbhUypU6&sender=IFFCOA&mobile=" + otpDetail.Mobileemail + "&text=" + otpDetail.Otp + "%20is%20the%20verification%20code%20generated%20for%20IFFCO%27s%20Recruitment%20Portal%20.%20Do%20not%20share%20this%20with%20anyone.%20IFFCO%20Aonla%20Unit&unicode=0&rpt=1&summary=1&output=json&entityid=1001232689878836835&templateid=1007642358221818606";

                                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                                StreamReader sr = new StreamReader(resp.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();

                                otpDetail.Smsapiresponse = results;
                                _context.SaveChanges();

                                // Call the function to send email
                                SendEmailToUser(EmailId);

                                return Ok(new { Message = "OTP sent to mobile number and email." });
                            }
                            catch (Exception ex)
                            {
                                // Log the exception for debugging
                                Console.WriteLine("Error sending OTP: " + ex.Message);
                                return BadRequest(new { Message = "Failed to send OTP." });
                            }
                        }
                    }
                    else
                    {
                        // Check the number of OTPs sent for the given mobile number in the last 24 hours
                        int maxAttemptsPerDay = 3;
                        DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

                        int sentOtpsCount = _context.RecOtpDetails
                            .Count(o => o.Mobileemail == MobileNumber && o.Otpdate >= twentyFourHoursAgo);

                        if (sentOtpsCount >= maxAttemptsPerDay)
                        {
                            return BadRequest(new { Message = "Exceeded OTP sending limit for the day. Only 3 SMS allowed." });
                        }

                        try
                        {
                            int newSerialNo = GenerateUniqueSerialNumber();
                            string otp = GenerateOTP();

                            var otpDetail = new RecOtpDetails()
                            {
                                Sno = newSerialNo,
                                Mobileemail = MobileNumber,
                                Otp = otp,
                                Otpdate = DateTime.Now,
                                Statuscode = EmailId,
                            };

                            _context.RecOtpDetails.Add(otpDetail);
                            _context.SaveChanges();

                            string URL = "http://hindit.biz/api/pushsms?user=Iffco&authkey=92QZpEbhUypU6&sender=IFFCOA&mobile=" + otpDetail.Mobileemail + "&text=" + otpDetail.Otp + "%20is%20the%20verification%20code%20generated%20for%20IFFCO%27s%20Recruitment%20Portal%20.%20Do%20not%20share%20this%20with%20anyone.%20IFFCO%20Aonla%20Unit&unicode=0&rpt=1&summary=1&output=json&entityid=1001232689878836835&templateid=1007642358221818606";

                            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                            StreamReader sr = new StreamReader(resp.GetResponseStream());
                            string results = sr.ReadToEnd();
                            sr.Close();

                            otpDetail.Smsapiresponse = results;
                            _context.SaveChanges();

                            // Call the function to send email
                            SendEmailToUser(EmailId);

                            return Ok(new { Message = "OTP sent to mobile number and email." });
                        }
                        catch (Exception ex)
                        {
                            // Log the exception for debugging
                            Console.WriteLine("Error sending OTP: " + ex.Message);
                            return BadRequest(new { Message = "Failed to send OTP." });
                        }
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Mobile number should have 10 digits." });
                }
            }
            else
            {
                return BadRequest(new { Message = "Failed to send OTP." });
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public void SendEmailToUser(string email)
        {
            string result = sendEmailtouser();
            // You can handle the result as needed
            Console.WriteLine("Email sending result: " + result);
        }

        public string sendEmailtouser()
        {
            try
            {
                // Assuming _context is an instance of your database context
                List<OracleParameter> oracleParameterCollecion = new List<OracleParameter>
        {
            new OracleParameter() { ParameterName = "I_CONTENT_TYPE", OracleDbType = OracleDbType.VarChar, Value = "" },
            new OracleParameter() { ParameterName = "I_CONTENT_ID", OracleDbType = OracleDbType.Number, Value = "" },
        };

                int a = _context.ExecuteProcedure("ATRMS_SEND_EMAIL_REC_AN_OTP", oracleParameterCollecion);
                string result = Convert.ToString(oracleParameterCollecion[2].Value);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine("Error sending email: " + ex.Message);
                return "Error: " + ex.Message; // Or you can throw the exception if you want it to propagate further
            }
        }


        private string GenerateOTP()
        {
            // Implement your OTP generation logic here
            // Example: Generate a random 6-digit OTP
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }
        private int GenerateUniqueSerialNumber()
        {
            // Query the maximum serial number from the database
            int maxSerialNo = _context.RecOtpDetails.Max(r => (int?)r.Sno) ?? 0;

            // Increment the maximum serial number
            int newSerialNo = maxSerialNo + 1;

            return newSerialNo;
        }

        [HttpPost]
        public IActionResult verifyOTP(string MobileNumber, string enteredOTP)
        {
            if (!string.IsNullOrEmpty(MobileNumber) && !string.IsNullOrEmpty(enteredOTP))
            {
                // Retrieve the latest OTP details for the given MobileNumber from the database
                var latestOtpDetail = _context.RecOtpDetails
                    .Where(otp => otp.Mobileemail == MobileNumber)
                    .OrderByDescending(otp => otp.Otpdate)
                    .FirstOrDefault();

                if (latestOtpDetail != null && latestOtpDetail.Otp == enteredOTP)
                {
                    // Check if the OTP is still valid (within 5 minutes)
                    var currentTime = DateTime.Now;

                    // Ensure that latestOtpDetail.Otpdate is a DateTime object
                    if (latestOtpDetail.Otpdate is DateTime otpTimestamp)
                    {
                        if ((currentTime - otpTimestamp).TotalMinutes <= 5)
                        {
                            return Ok("OTP verification successful.");
                        }
                        else
                        {
                            // Expired OTP
                            return BadRequest("OTP has expired.");
                        }
                    }
                    else
                    {
                        // Handle the case where Otpdate is not a DateTime
                        return BadRequest("Invalid timestamp for OTP.");
                    }
                }

                return BadRequest("Invalid OTP.");
            }

            return BadRequest("Mobile number and OTP are required.");
        }




    }
}

