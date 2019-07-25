using ByjuAPI.Manager;
using ByjuAPI.Models;
using ByjuAPI.ViewModel;
using System.Web.Http;

namespace ByjuAPI.Controllers
{
    public class AccountController : ApiController
    {
        AccountManager accountManager = new AccountManager();

        /// <summary>
        /// API for user registration
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [Route("api/Account/registerUser")]
        [HttpPost]
        public ApiResponse Register(RegisterViewModel registerModel)
        {
            return accountManager.RegisterUser(registerModel);
        }

        /// <summary>
        /// API for user login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [Route("api/Account/loginUser")]
        [HttpPost]
        public ApiResponse Login(LoginViewModel loginModel)
        {
            return accountManager.LoginUser(loginModel);
        }

        /// <summary>
        /// API for sending account activation email for the recently registered user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/Account/sendActivateAccountEmail")]
        [HttpPost]
        public ApiResponse VerifyEmail(string email)
        {
            return accountManager.VerifyUserEmail(email);
        }

        /// <summary>
        /// API for setting the isVerified Flag to activate the recently registered user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/Account/activateAccount")]
        [HttpPost]
        public ApiResponse ActivateAccount(string email)
        {
            return accountManager.ActivateAccount(email);
        }

        /// <summary>
        /// API for sending the reset password email to the requesting user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/Account/sendResetPasswordEmail")]
        [HttpPost]
        public ApiResponse ResetPassword(string email)
        {
            return accountManager.ResetPassword(email);
        }

        /// <summary>
        /// API for changing the password for the requesting user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [Route("api/Account/changePassword")]
        [HttpPost]
        public ApiResponse ChangePassword(LoginViewModel loginModel)
        {
            return accountManager.ChangePassword(loginModel);
        }
    }
}
