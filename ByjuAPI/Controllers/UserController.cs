using ByjuAPI.Manager;
using ByjuAPI.Models;
using System.Web.Http;

namespace ByjuAPI.Controllers
{
    public class UserController : ApiController
    {
        UserManager userManager = new UserManager();

        /// <summary>
        /// API for fetching all the registered users
        /// </summary>
        /// <returns></returns>
        [Route("api/User/getAllUsers")]
        [HttpGet]
        public ApiResponse GetAllUsers()
        {
            return userManager.GetAllUsers();
        }

        /// <summary>
        /// API for fetching the user details by his email Id
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [Route("api/User/getUserByEmail")]
        [HttpGet]
        public ApiResponse GetUserByEmail(string userEmail)
        {
            return userManager.GetUserByEmail(userEmail);
        }
    }
}
