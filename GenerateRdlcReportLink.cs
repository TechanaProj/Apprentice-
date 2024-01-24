using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IFFCO.Web
{
    public class GenerateRdlcReportLink
    {
        private string reportrdlcUrl = "https://appsaonla.iffco.coop/RDLC/REC_RDLC";
        public string GenerateLink(string Report_Name, string Query_String, string AreaName)
        {
            string Report = "";
            string data = string.Empty;
            string QueryString = Query_String;
            data = Report_Name;

            Report = GenerateReportRdlc(QueryString, data, AreaName);
            return Report;
        }

        public string GenerateReportRdlc(string querystring, string reportname, string module)
        {
            string report = "";
            report = reportrdlcUrl + "/" + module + "/" + reportname + "?" + Encclass.GetEncryptedQueryString(querystring.Replace("''", ""));
            return report;
        }
    }
}
