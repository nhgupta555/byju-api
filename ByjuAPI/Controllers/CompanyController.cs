using ByjuAPI.Manager;
using ByjuAPI.Models;
using ByjuAPI.ViewModel;
using System.Collections.Generic;
using System.Web.Http;

namespace ByjuAPI.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        CompanyManager companyManager = new CompanyManager();

        /// <summary>
        /// API for registering a new company
        /// </summary>
        /// <param name="companyModel"></param>
        /// <returns></returns>
        [Route("api/Company/registerCompany")]
        [HttpPost]
        public ApiResponse RegisterCompany(CompanyViewModel companyModel)
        {
            return companyManager.RegisterCompany(companyModel);
        }

        /// <summary>
        /// API for fecthing all the registered companies
        /// </summary>
        /// <returns></returns>
        [Route("api/Company/getAllCompanies")]
        [HttpGet]
        public ApiResponse GetAllCompanies()
        {
            return companyManager.GetAllCompanies();
        }

        /// <summary>
        /// API for adding the registered companies to user's favourite list
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <param name="companyNames"></param>
        /// <returns></returns>
        [Route("api/Company/markUnmarkFavouriteCompany")]
        [HttpPut]
        public ApiResponse AddCompanyToFavorites(string loggedInUserEmail, List<Company> companyNames)
        {
            return companyManager.AddCompanyToFavorites(loggedInUserEmail, companyNames);
        }

        /// <summary>
        /// API for fetching all the favourite companies for a particular user
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <returns></returns>
        [Route("api/Company/getFavouriteCompanies")]
        [HttpGet]
        public ApiResponse GetFavouriteCompanies(string loggedInUserEmail)
        {
            return companyManager.GetFavouriteCompanies(loggedInUserEmail);
        }
    }
}
