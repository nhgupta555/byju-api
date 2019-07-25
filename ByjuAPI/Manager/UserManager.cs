using ByjuAPI.Helper;
using ByjuAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByjuAPI.Manager
{
    public class UserManager
    {
        DBHelper dbHelper = new DBHelper();

        /// <summary>
        /// Manager method to fetch all the registered users
        /// </summary>
        /// <returns></returns>
        public ApiResponse GetAllUsers()
        {
            return dbHelper.GetAllUsers();
        }

        /// <summary>
        /// Manager method to fetch user details by email Id
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public ApiResponse GetUserByEmail(string userEmail)
        {
            return dbHelper.GetUserByEmail(userEmail);
        }
    }
}