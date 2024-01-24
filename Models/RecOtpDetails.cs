using System;
using System.Collections.Generic;

namespace USERFORM.Models
{
    public partial class RecOtpDetails
    {
        public decimal Sno { get; set; }
        public string Mobileemail { get; set; }
        public string Otp { get; set; }
        public DateTime? Otpdate { get; set; }
        public string Statuscode { get; set; }
       
        public string Smsapiresponse { get; set; }
    }
}
