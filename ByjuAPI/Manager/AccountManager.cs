using ByjuAPI.Helper;
using ByjuAPI.Models;
using ByjuAPI.ViewModel;
using System.Web.SessionState;
using System.Web;

namespace ByjuAPI.Manager
{
    public class AccountManager
    {
        DBHelper dbHelper = new DBHelper();
        EmailHelper emailHelper = new EmailHelper();
        TokenManager tokenManager = new TokenManager();

        /// <summary>
        /// Manager method for user registration
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public ApiResponse RegisterUser(RegisterViewModel registerModel)
        {
            User user = new User()
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                Password = registerModel.Password
            };

            var response = emailHelper.SendVerificationEmail(user.Email);
            if (response.IsSuccess == true)
            {
                response = dbHelper.RegisterUser(user);             
            }

            return response;
        }

        /// <summary>
        /// Manager method for user login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public ApiResponse LoginUser(LoginViewModel loginModel)
        {
            User user = new User()
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            };
            var response = dbHelper.LoginUser(user);
            if(response.IsSuccess)
            {
                var token = tokenManager.GenerateToken(loginModel.Email);
                var session = HttpContext.Current.Session;
                if(session != null)
                {
                    session["UserToken"] = token;
                }
                response.ReturnValue = token;
                response.AuthToken = token;
            }

            return response;
        }

        /// <summary>
        /// Manager method for verifying user email for account activation for recently registered user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse VerifyUserEmail(string email)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse = emailHelper.SendVerificationEmail(email);
            if(apiResponse.IsSuccess)
            {
                apiResponse.Message = "Account Activation Link is emailed successfully. Please check your email and activate your Byju's Account.";
            }
            else
            {
                if(string.IsNullOrEmpty(apiResponse.Message))
                {
                    apiResponse.Message = "Email Verification Failed.";
                }         
            }

            return apiResponse;
        }

        /// <summary>
        /// Manager method for account activation for recently registered user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse ActivateAccount(string email)
        {
            return dbHelper.ActivateAccount(email);
        }

        /// <summary>
        /// Manager method for sending reset password email for requesting user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse ResetPassword(string email)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse = emailHelper.SendResetPasswordEmail(email);
            if(apiResponse.IsSuccess)
            {
                apiResponse.Message = "Reset Password Link is emailed successfully. Please check your email and reset your password for Byju's Account.";
            }
            else
            {
                if (string.IsNullOrEmpty(apiResponse.Message))
                {
                    apiResponse.Message = "Password Reset Failed.";
                }               
            }

            return apiResponse;
        }

        /// <summary>
        /// Manager method for resetting password in database for requesting user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public ApiResponse ChangePassword(LoginViewModel loginModel)
        {
            User user = new User()
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            };

            return dbHelper.ChangePassword(user);
        }
    }
}