using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class AtrmsEmailLog
    {
        public long? SeqNo { get; set; }
        public string AtId { get; set; }
        public decimal? RollNo { get; set; }
        public string RecCode { get; set; }
        public DateTime? RecordDt { get; set; }
        public string ToEmail { get; set; }
        public string ToEmail2 { get; set; }
        public string ToEmail3 { get; set; }
        public string CcEmail { get; set; }
        public string CcEmail2 { get; set; }
        public string CcEmail3 { get; set; }
        public string FromAddress { get; set; }
        public string MailSubj { get; set; }
        public string MailBody { get; set; }
        public string Module { get; set; }
        public DateTime? RequestDatetime { get; set; }
        public DateTime? SendingDatetime { get; set; }
        public string EmailStatus { get; set; }
        public string EmailAttempt { get; set; }
        public string Projectid { get; set; }
        public string ErrorMsg { get; set; }
        public string MailHtml { get; set; }
    }
}
