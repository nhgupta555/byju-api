using ByjuAPI.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ByjuAPI.Helper
{
    public class EmailHelper
    {
        /// <summary>
        /// Helper method for sending email for account activation for recently registered user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse SendVerificationEmail(string email)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.IsSuccess = false;

            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["fromEmail"], ConfigurationManager.AppSettings["fromUserName"]);
                var toAddress = new MailAddress(email);
                string fromPassword = ConfigurationManager.AppSettings["fromPassword"];
                string subject = "Byju Account Activation";
                string body = "Please click the link below to complete your account activation <br/> <a href=\'http://localhost:4200/verify-account' >Verify Email Account</a>";
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["gmailHost"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["gmailPortNumber"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(mailMessage);
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "Email Sent Successfully.";
                }
            }
            catch(Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Registration Failed." + ex.Message;
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method for sending email for resetting password for the requesting user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse SendResetPasswordEmail(string email)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.IsSuccess = false;

            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["fromEmail"], ConfigurationManager.AppSettings["fromUserName"]);
                var toAddress = new MailAddress(email);
                string fromPassword = ConfigurationManager.AppSettings["fromPassword"];
                string subject = "Byju Account Reset Password";
                string body = "Please click the link below to reset your account password <br/> <a href=\'http://localhost:4200/change-password' >Reset Password</a>";
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["gmailHost"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["gmailPortNumber"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(mailMessage);
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "Email Sent Successfully.";
                }
            }
            catch(Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
    }
}