using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using USERFORM.Models;
using USERFORM.ViewModels;

namespace USERFORM.Controllers
{
    public class AccountController : Controller

    {

        private readonly ModelContext _context;
        public IActionResult Login()
        {
            return View();
        }
        public AccountController(ModelContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> sendOTP(string MobileNumber)
        {
            USERF01ViewModel uSERF01ViewModel = new USERF01ViewModel();

            if (!string.IsNullOrEmpty(MobileNumber))
            {
                // Validate if the MobileNumber has exactly 10 digits
                if (MobileNumber.Length == 10 && MobileNumber.All(char.IsDigit))
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

                    string statusCode = "S";
                    int newSerialNo = GenerateUniqueSerialNumber();
                    string otp = GenerateOTP();
                    string smsContents = $"Your One-Time Password is {otp}, valid for 30 minutes only. Please do not share your OTP.";

                    // Create an instance without Smsapiresponse initially
                    var otpDetail = new RecOtpDetails()
                    {
                        Sno = newSerialNo,
                        Mobileemail = MobileNumber,
                        Otp = otp,
                        Otpdate = DateTime.Now,
                        Statuscode = statusCode,
                    };

                    // Add the instance to the context without Smsapiresponse
                    _context.RecOtpDetails.Add(otpDetail);
                    _context.SaveChanges();

                   

                    return Ok(new { Message = "OTP sent successfully." });
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

        public IActionResult verifyOTP(USERF01ViewModel Obj)
        {
            if (!string.IsNullOrEmpty(Obj.MobileNumber) && !string.IsNullOrEmpty(Obj.enteredOTP))
            {
                // Retrieve the latest OTP details for the given MobileNumber from the database
                var latestOtpDetail = _context.RecOtpDetails
                    .Where(otp => otp.Mobileemail == Obj.MobileNumber)
                    .OrderByDescending(otp => otp.Otpdate)
                    .FirstOrDefault();

                if (latestOtpDetail != null && latestOtpDetail.Otp == Obj.enteredOTP)
                {
                    // Check if the OTP is still valid (within 5 minutes)
                    var currentTime = DateTime.Now;

                    // Ensure that latestOtpDetail.Otpdate is a DateTime object
                    if (latestOtpDetail.Otpdate is DateTime otpTimestamp)
                    {
                        if ((currentTime - otpTimestamp).TotalMinutes <= 5)
                        {
                            // Valid OTP, you can perform additional actions here
                            //return Ok("OTP verification successful.");
                            return RedirectToAction("Create", "USERF01", new { area = "M1" });

                        }
                        else
                        {
                            // Expired OTP
                            return BadRequest("OTP has expired.");
                        }
                    }
                    else
                    {
                       
                        return BadRequest("Invalid timestamp for OTP.");
                    }
                }

                return BadRequest("Invalid OTP.");
            }

            return BadRequest("Mobile number and OTP are required.");
        }



    }
}
