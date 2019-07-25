using ByjuAPI.Helper;
using ByjuAPI.Models;
using ByjuAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByjuAPI.Manager
{
    public class CompanyManager
    {
        DBHelper dbHelper = new DBHelper();

        /// <summary>
        /// Manager method for a new company registration
        /// </summary>
        /// <param name="companyModel"></param>
        /// <returns></returns>
        public ApiResponse RegisterCompany(CompanyViewModel companyModel)
        {
            Company company = new Company()
            {
                Name = companyModel.Name,
                Address = companyModel.Address,
                PhoneNumber = companyModel.PhoneNumber
            };

            return dbHelper.RegisterCompany(company);
        }

        /// <summary>
        /// Manager methods for fetching all the registered companies
        /// </summary>
        /// <returns></returns>
        public ApiResponse GetAllCompanies()
        {
            return dbHelper.GetAllCompanies();
        }

        /// <summary>
        /// Manager methods for adding companies to user's favourite list
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <param name="companyNames"></param>
        /// <returns></returns>
        public ApiResponse AddCompanyToFavorites(string loggedInUserEmail, List<Company> companyNames)
        {
            return dbHelper.AddCompanyToFavorites(loggedInUserEmail, companyNames);
        }

        /// <summary>
        /// Manager methods for fetching user's favourite companies
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <returns></returns>
        public ApiResponse GetFavouriteCompanies(string loggedInUserEmail)
        {
            return dbHelper.GetFavouriteCompanies(loggedInUserEmail);
        }
    }
}