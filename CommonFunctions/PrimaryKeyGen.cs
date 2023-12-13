using USERFORM.Models;

using System;
using System.Linq;


namespace USERFORM.CommonFunctions
{
    public class PrimaryKeyGen
    {

        private readonly ModelContext _context;



        public PrimaryKeyGen()
        {
            _context = new ModelContext();
        }

        //public string Get_EnrolledATCode_PK(int unit)
        //{
        //    int AMax = 0;
        //    int BMax = 0;

        //    // Query the database for the maximum "AtId" values for the given "unit"
        //    var maxValues = _context.AtrmsPersonalDtl
        //        .Where(x => x.UnitCode == unit)
        //        .Select(x => x.AtId)
        //        .ToList();

        //    if (maxValues.Any())
        //    {
        //        // Get the maximum "AtId" value from the list
        //        AMax = maxValues.Max(AtId => int.TryParse(AtId.Substring(3, 3), out int parsedValue) ? parsedValue : 0);
        //        string financialYear = (DateTime.Today.AddMonths(-3).Year % 100).ToString("00");
        //    }

        //    BMax = AMax + 1;

        //    // Construct the code based on the current year minus 3 months and the incremented value
        //    string code = financialYear + "AT" + BMax.ToString().PadLeft(4, '0');

        //    return code;
        //}
        public string Get_EnrolledATCode_PK(int unit)
        {
            int AMax = 0;
            int BMax = 0;

            // Query the database for the maximum "AtId" values for the given "unit"
            var maxValues = _context.AtrmsPersonalDtl
                .Where(x => x.UnitCode == unit)
                .Select(x => x.AtId)
                .ToList();

            if (maxValues.Any())
            {
                // Get the maximum "AtId" value from the list
                AMax = maxValues.Max(AtId => int.TryParse(AtId.Substring(6, 4), out int parsedValue) ? parsedValue : 0);
            }

            BMax = AMax + 1;

            // Get the current year minus 3 months
            string financialYear = (DateTime.Today.AddMonths(-3).Year % 100).ToString("00");
            string nextYear = (DateTime.Today.AddMonths(9).Year % 100).ToString("00");
            string financialYearRange = financialYear + nextYear;

            // Construct the code by combining the parts
            string code = financialYearRange + "AT" + BMax.ToString().PadLeft(4, '0');

            return code;
        }


    }



}
