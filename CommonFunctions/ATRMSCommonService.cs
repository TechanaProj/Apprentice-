using Devart.Data.Oracle;
using USERFORM.Models;
using USERFORM.ViewModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace USERFORM.CommonFunctions
{
    public class ATRMSCommonService
    {
        private readonly ModelContext _context;

        public ATRMSCommonService()
        {
            _context = new ModelContext();
        }

        public List<RecCategoryMsts> GetCategoryMaster(string RecCode)
        {
            string sqlquery = "SELECT CATEGORY_CODE, CATEGORY, DECEASED, LANDLOSER, EX_APP, MIN_AGE, MAX_AGE, ON_DATE, MIN_DATE, MAX_DATE, QUALIFYING_MARKS_GENERAL_OBC, QUALIFYING_MARKS_SC_ST, MIN_PASSED_YEAR, REC_CODE FROM RECAN.REC_CATEGORY_MSTS WHERE REC_CODE = '" + RecCode + "'";

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
                             OnDate = Convert.ToDateTime(dr["ON_DATE"]),
                             MinDate = Convert.ToDateTime(dr["MIN_DATE"]),
                             MaxDate = Convert.ToDateTime(dr["MAX_DATE"]),
                             QualifyingMarksGeneralObc = (int.TryParse(Convert.ToString(dr["QUALIFYING_MARKS_GENERAL_OBC"]), out int obcResult)) ? obcResult : 0,
                             QualifyingMarksScSt = (int.TryParse(Convert.ToString(dr["QUALIFYING_MARKS_SC_ST"]), out int scstResult)) ? scstResult : 0,
                             MinPassedYear = Convert.ToInt32(dr["MIN_PASSED_YEAR"]),
                             RecCode = Convert.ToString(dr["REC_CODE"])
                         }).ToList();

            return DTL_VALUE;
        }

        public List<RecCategoryMsts> GetRecCodeMsts(string Reccode)
        {
            string sqlquery = "SELECT rcg.REC_CODE, rc.CATEGORY_CODE, rc.CATEGORY, rc.DECEASED, rc.LANDLOSER, rc.EX_APP, rc.MIN_AGE, rc.MAX_AGE, rc.ON_DATE, rc.MIN_DATE, rc.MAX_DATE, rc.QUALIFYING_MARKS_GENERAL_OBC, rc.QUALIFYING_MARKS_SC_ST, rc.MIN_PASSED_YEAR, rc.CREATED_BY, rc.CREATED_DATETIME, rc.MODIFICATION_DT, rc.MODIFIED_BY  FROM REC_CODE_GENERATION_MSTS rcg LEFT JOIN REC_CATEGORY_MSTS rc ON rcg.REC_CODE = rc.REC_CODE WHERE rc.CATEGORY_CODE IS NULL AND rcg.REC_CODE '" + Reccode + "' ";

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
                             OnDate = Convert.ToDateTime(dr["ON_DATE"]),
                             MinDate = Convert.ToDateTime(dr["MIN_DATE"]),
                             MaxDate = Convert.ToDateTime(dr["MAX_DATE"]),
                             QualifyingMarksGeneralObc = (int.TryParse(Convert.ToString(dr["QUALIFYING_MARKS_GENERAL_OBC"]), out int obcResult)) ? obcResult : 0,
                             QualifyingMarksScSt = (int.TryParse(Convert.ToString(dr["QUALIFYING_MARKS_SC_ST"]), out int scstResult)) ? scstResult : 0,
                             MinPassedYear = Convert.ToInt32(dr["MIN_PASSED_YEAR"]),
                             RecCode = Convert.ToString(dr["REC_CODE"])
                         }).ToList();

            return DTL_VALUE;

        }

        public List<RecCategoryMsts> GetCategoryMsts()
        {
            string sqlquery = "SELECT DISTINCT  CATEGORY_CODE, CATEGORY, DECEASED, LANDLOSER, EX_APP, MIN_AGE, MAX_AGE FROM REC_CATEGORY_MSTS ORDER BY CATEGORY_CODE ASC ";

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






    }
}