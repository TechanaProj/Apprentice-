
using USERFORM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System.Security.Cryptography.X509Certificates;

namespace USERFORM.CommonFunctions
{
    public class DropDownListBindWeb
    {
       // private readonly IRepositoryProvider _repositoryProvider = new RepositoryProvider(new RepositoryFactories());

        //private readonly IUnitOfWorkAsync _unitOfWork;

        private readonly ModelContext _context;
        DataTable _dt = new DataTable();

        public List<SelectListItem> RecCodeLOV { get; private set; }

        public DropDownListBindWeb()
        {
            _context = new ModelContext();
        }




        public List<SelectListItem> GET_Review()
        {
            var Status = new List<SelectListItem>();
            Status = new List<SelectListItem>();
            Status.Add(new SelectListItem { Text = "--Select--", Value = "" });
            Status.Add(new SelectListItem { Text = "Excellent", Value = "Excellent" });
            Status.Add(new SelectListItem { Text = "Good", Value = "Good" });
            Status.Add(new SelectListItem { Text = "Average", Value = "Average" });
            Status.Add(new SelectListItem { Text = "Not Applicable", Value = "Not Applicable" });

            return Status;

        }

        public List<SelectListItem> GET_APPTRADESTATUS()
        {
            var Status = new List<SelectListItem>();
            Status = new List<SelectListItem>();
            Status.Add(new SelectListItem { Text = "--Select--", Value = "" });
            Status.Add(new SelectListItem { Text = "Y - YES", Value = "A" });
            Status.Add(new SelectListItem { Text = "N - NO", Value = "I" });
            return Status;
        }

        public List<SelectListItem> GET_CATEGORY()
        {
            var Status = new List<SelectListItem>();
            Status = new List<SelectListItem>();
            Status.Add(new SelectListItem { Text = "--Select--", Value = "" });
            Status.Add(new SelectListItem { Text = "Genral", Value = "Genral" });
            Status.Add(new SelectListItem { Text = "OBC", Value = "OBC" });
            Status.Add(new SelectListItem { Text = "SC/ST", Value = "SC/ST" });
            return Status;
        }
        //public List<SelectListItem> RecCodeLOVBind()
        //{
        //    var listState = _context.RecCodeGenerationMsts.Where(x => x.RecStatus.Equals("A")).OrderBy(x => x.RecCode).Select(x => new SelectListItem
        //    {
        //        Text = string.Concat(x.RecCode),
        //        Value = x.RecCode.ToString()
        //    }).ToList();
        //    return listState;
            
        //}
        public List<SelectListItem> RecCodeLOVBind()
        {
            // Start with a "Select" option
            var listState = new List<SelectListItem>
    {
        new SelectListItem
        {
            Text = "Select",
            Value = ""
        }
    };

            // Add the actual data from the database
            listState.AddRange(_context.RecCodeGenerationMsts
                .Where(x => x.RecStatus.Equals("A"))
                .OrderBy(x => x.RecCode)
                .Select(x => new SelectListItem
                {
                    Text = string.Concat(x.RecCode),
                    Value = x.RecCode.ToString()
                })
                .ToList());

            return listState;
        }


        public List<SelectListItem> RecCodeCategLOVBind()
        {
            string sqlquery = "SELECT rcg.REC_CODE FROM REC_CODE_GENERATION_MSTS rcg LEFT JOIN REC_CATEGORY_MSTS rc ON rcg.REC_CODE=rc.REC_CODE WHERE rc.CATEGORY_CODE IS NULL ORDER BY rCG.REC_CODE ASC";
            DataTable dtDRP_VALUE = _context.GetSQLQuery(sqlquery);
            var DRP_VALUE = (from DataRow dr in dtDRP_VALUE.Rows
                             select new SelectListItem
                             {
                                 Text = Convert.ToString(dr["REC_CODE"]),
                                 Value = Convert.ToString(dr["REC_CODE"])
                             }).ToList();
            return DRP_VALUE;
        }

        //public List<SelectListItem> CategoryCodelist(string CATEGORY_CODE)
        //{

        //        string sqlquery = "SELECT DISTINCT '' rec_code, CATEGORY_CODE, CATEGORY, DECEASED, LANDLOSER, EX_APP, MIN_AGE, MAX_AGE FROM REC_CATEGORY_MSTS ORDER BY CATEGORY_CODE ASC";

        //    DataTable dtDRP_VALUE = _context.GetSQLQuery(sqlquery);
        //    var DRP_VALUE = (from DataRow dr in dtDRP_VALUE.Rows
        //                     select new RecCategoryMsts()
        //                     {
        //                         CategoryCode = Convert.ToString(dr["CATEGORY_CODE"]),
        //                         Category = Convert.ToString(dr["CATEGORY"]),
        //                         Deceased = Convert.ToString(dr["DECEASED"]),
        //                         Landloser = Convert.ToString(dr["LANDLOSER"]),
        //                         ExApp = Convert.ToString(dr["EX_APP"]),
        //                         MinAge = Convert.ToInt32(dr["MIN_AGE"]),
        //                         MaxAge = Convert.ToInt32(dr["MAX_AGE"]),
        //                     }).ToList();
        //    return DRP_VALUE;
        //}

        public List<RecCategoryMsts> CategoryCodelist(string CATEGORY_CODE)
        {
            string sqlquery = "SELECT DISTINCT CATEGORY_CODE, CATEGORY, DECEASED, LANDLOSER, EX_APP FROM REC_CATEGORY_MSTS ORDER BY CATEGORY_CODE ASC AND CATEGORY_CODE '" + CATEGORY_CODE + "' ";

            DataTable dtDTL_VALUE = new DataTable();
            dtDTL_VALUE = _context.GetSQLQuery(sqlquery);
            List<RecCategoryMsts> DTL_VALUE = new List<RecCategoryMsts>();
            DTL_VALUE = (from DataRow dr in dtDTL_VALUE.Rows
                         select new RecCategoryMsts()
                         {
                             CategoryCode = Convert.ToString(dr["CATEGORY_CODE"]),
                             Category = Convert.ToString(dr["CATEGORY"]),
                             Deceased = Convert.ToString(dr["DECEASED"]),
                             Landloser = Convert.ToString(dr["LANDLOSER"]),
                             ExApp = Convert.ToString(dr["EX_APP"]),
                             MinAge = Convert.ToInt32(dr["MIN_AGE"]),
                             MaxAge = Convert.ToInt32(dr["MAX_AGE"]),
                         }).ToList();

            return DTL_VALUE;

        }
        public List<SelectListItem> listStateBind()
        {
            var listState = _context.RecStateMsts.OrderBy(x => x.StateName).Select(x => new SelectListItem
            {
                Text = string.Concat(x.StateCd, " - ", x.StateName),
                Value = x.StateCd.ToString()
            }).ToList();
            return listState;
        }



        public List<SelectListItem> StateLOVBind()
        {
            var StateLOV = _context.RecStateMsts.OrderBy(x => x.StateName).Select(x => new SelectListItem
            {
                Text = string.Concat(x.StateCd, " - ", x.StateName),
                Value = x.StateCd.ToString()



            }).ToList();



            return StateLOV;
        }



        public List<SelectListItem> ListDistrictBind(string StateCd)
        {
            var listDistrict = _context.RecDistrictMsts.Where(x => x.StateCd == StateCd).OrderBy(x => x.DisttName).Select(x => new SelectListItem
            {
                Text = string.Concat(x.DisttCd, " - ", x.DisttName),
                Value = x.DisttCd.ToString()
            }).ToList();
            return listDistrict;
        }



        public List<SelectListItem> DistrictLOVBind()
        {
            var DistrictLOV = _context.RecDistrictMsts.OrderBy(x => x.DisttCd).Select(x => new SelectListItem
            {
                Text = string.Concat(x.DisttCd, " - ", x.DisttName),
                Value = x.DisttCd.ToString()



            }).ToList();



            return DistrictLOV;
        }

        public List<SelectListItem> POSTAPPLIEDDESCRIPTIONBind()
        {
            var POSTAPPLIEDDESCRIPTION = _context.RecPostAvailableMsts.OrderBy(x => x.PostAppliedDescription).Select(x => new SelectListItem
            {
                Text = string.Concat(x.PostAppliedCode, " - ", x.PostAppliedDescription),
                Value = x.PostAppliedCode.ToString()

            }).ToList();


            return POSTAPPLIEDDESCRIPTION;
        }
        public List<SelectListItem> POSTAPPLIEDCODEBind()
        {
            var POSTAPPLIEDCODE = _context.RecPostAvailableMsts.OrderBy(x => x.PostAppliedCode).Select(x => new SelectListItem
            {
                Text = string.Concat(x.PostAppliedCode),
                Value = x.PostAppliedCode.ToString()

            }).ToList();


            return POSTAPPLIEDCODE;
        }

        public List<SelectListItem> POSTAPPLIEDCODERECCODEBind()
        {
            var POSTAPPLIEDCODERECCODE = _context.RecPostAvailableMsts.OrderBy(x => x.RecCode).Select(x => new SelectListItem
            {
                Text = string.Concat(x.RecCode),
                Value = x.PostAppliedCode.ToString()

            }).ToList();


            return POSTAPPLIEDCODERECCODE;
        }

        public List<SelectListItem> PostdescriptionLOVBind()
        {
            var POSTDESCRIPTION = _context.RecPostAvailableMsts
                .Where(x => x.RecStatus == "A")
                .Select(x => new SelectListItem
                {
                    Text = string.Concat(x.PostAppliedDescription),
                    Value = x.PostAppliedCode.ToString() + "," + x.RecCode.ToString()
                })
                .ToList();

            // Add a "Select" item at the beginning of the list
            POSTDESCRIPTION.Insert(0, new SelectListItem
            {
                Text = "Select",
                Value = ""  // You can set the value to an appropriate default value or leave it empty
            });

            return POSTDESCRIPTION;
        }


        public List<AtrmsPersonalDtl> GetFilteredDataGendCate(string selectedGender, string selectedCategory)
        {
            var query = _context.AtrmsPersonalDtl
                .Where(x => (selectedGender == "All" || x.Gender == selectedGender) &&
                            (selectedCategory == "All" || x.Category == selectedCategory))
                .ToList();

            return query;
        }




    }

}