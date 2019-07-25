using ByjuAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ByjuAPI.Helper
{
    public class DBHelper
    {
        #region DB Helper variables

        private MongoClient Client;
        private IMongoDatabase Database;
        private IMongoCollection<User> UserCollection;
        private IMongoCollection<Company> CompanyCollection;

        #endregion

        #region Constructor

        public DBHelper()
        {
            Client = new MongoClient(ConfigurationManager.AppSettings["ConnectionString"]);
            Database = Client.GetDatabase(ConfigurationManager.AppSettings["DBName"]);
            UserCollection = Database.GetCollection<User>("User");
            CompanyCollection = Database.GetCollection<Company>("Company");
        }

        #endregion

        #region Account Methods

        /// <summary>
        /// Helper method for user registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResponse RegisterUser(User user)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                bool isUserExist = UserCollection.Find(x => x.Email == user.Email).Any();

                if (isUserExist)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Already Registered User";
                }
                else
                {
                    user.isVerifiedEmail = false;
                    user.favoriteCompanies = new List<string>();
                    UserCollection.InsertOne(user);
                    apiResponse.IsSuccess = true;
                    apiResponse.ReturnValue = user._id;
                    apiResponse.Message = "User Registration Successful";
                }
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                apiResponse.IsSuccess = false;
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method for user login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResponse LoginUser(User user)
        {
            ApiResponse apiResponse = new ApiResponse();
            var loggedInUser = UserCollection.Find(x => x.Email == user.Email && x.Password == user.Password).SingleOrDefault();

            if (ValidateEmail(user.Email))
            {
                if (loggedInUser.isVerifiedEmail == true)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.ReturnValue = loggedInUser._id;
                    apiResponse.Message = "Logged In Successfully";
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Please check your email and activate your Byju's Account before Login. If not received activation email please click on Resend Activation Email";
                }

            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Login Failed. Incorrect Email or Password";
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method for validating the email is registerd or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        {
            return UserCollection.Find(x => x.Email == email).Any();
        }

        /// <summary>
        /// Helper method for account verification for recently registerd user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApiResponse ActivateAccount(string email)
        {
            ApiResponse apiResponse = new ApiResponse();

            var user = UserCollection.Find(x => x.Email == email);
            if (user.Any())
            {
                UserCollection.UpdateOne(
                     Builders<User>.Filter.Eq(x => x._id, user.SingleOrDefault()._id),
                     Builders<User>.Update.Set(x => x.isVerifiedEmail, true),
                     new UpdateOptions { IsUpsert = true }
                    );

                apiResponse.IsSuccess = true;
                apiResponse.Message = "Account Verified Successfully";
            }
            else
            {
                apiResponse.IsSuccess = true;
                apiResponse.Message = "Account Verification Failed";
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method for change password for the requesting user
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public ApiResponse ChangePassword(User currentUser)
        {
            ApiResponse apiResponse = new ApiResponse();

            var user = UserCollection.Find(x => x.Email == currentUser.Email);
            if (user.Any())
            {
                if (user.SingleOrDefault().isVerifiedEmail)
                {
                    UserCollection.UpdateOne(
                    Builders<User>.Filter.Eq(x => x._id, user.SingleOrDefault()._id),
                    Builders<User>.Update.Set(x => x.Password, currentUser.Password),
                    new UpdateOptions { IsUpsert = true }
                   );

                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "Password Reset Successful";
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "User Account is not yet verified.";
                }
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "User Not Found.";
            }

            return apiResponse;
        }

        #endregion

        #region User Methods

        /// <summary>
        /// Helper methods for fetching all registered users
        /// </summary>
        /// <returns></returns>
        public ApiResponse GetAllUsers()
        {
            ApiResponse apiResponse = new ApiResponse();

            var users = UserCollection.Find(FilterDefinition<User>.Empty);
            if (users != null)
            {
                apiResponse.IsSuccess = true;
                apiResponse.ReturnValue = users.ToList();
                apiResponse.Message = "Get All Users";
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "No Users Found";
            }

            return apiResponse;

        }

        /// <summary>
        /// Helper methods for fecthing user details by email Id
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public ApiResponse GetUserByEmail(string userEmail)
        {
            ApiResponse apiResponse = new ApiResponse();

            var user = UserCollection.Find(u => u.Email == userEmail);
            if (user.Any())
            {
                apiResponse.IsSuccess = true;
                apiResponse.ReturnValue = user.SingleOrDefault();
                apiResponse.Message = "User Search Successful";
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "User Not Found";
            }

            return apiResponse;
        }

        #endregion

        #region Company Methods

        /// <summary>
        /// Helper method for registering a company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public ApiResponse RegisterCompany(Company company)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                bool isCompanyExist = CompanyCollection.Find(x => x.Name == company.Name).Any();
                if (isCompanyExist)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = "Already Registered Company";
                }
                else
                {
                    company.isFavourite = false;
                    CompanyCollection.InsertOne(company);
                    apiResponse.IsSuccess = true;
                    apiResponse.ReturnValue = company.Name;
                    apiResponse.Message = "Company Registration Successful";
                }

            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                apiResponse.IsSuccess = false;
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method for fetching all companies
        /// </summary>
        /// <returns></returns>
        public ApiResponse GetAllCompanies()
        {
            ApiResponse apiResponse = new ApiResponse();

            var companies = CompanyCollection.Find(FilterDefinition<Company>.Empty);
            if (companies != null)
            {
                apiResponse.IsSuccess = true;
                apiResponse.ReturnValue = companies.ToList();
                apiResponse.Message = "Get All Companies";
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "No Companies Found";
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method to add/remove companies from user's favourite list
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <param name="companyNames"></param>
        /// <returns></returns>
        public ApiResponse AddCompanyToFavorites(string loggedInUserEmail, List<Company> companyNames)
        {
            ApiResponse apiResponse = new ApiResponse();

            var user = UserCollection.Find(x => x.Email == loggedInUserEmail);
            List<string> favCompanyNames = new List<string>();

            if (user != null)
            {
                foreach (var company in companyNames)
                {                   
                    if (company.isFavourite)
                        favCompanyNames.Add(company.Name);
                    CompanyCollection.UpdateOne(
                        Builders<Company>.Filter.Eq(x => x._id, company._id),
                        Builders<Company>.Update.Set(x => x.isFavourite, company.isFavourite),
                        new UpdateOptions { IsUpsert = true }
                        );
                }

                UserCollection.UpdateOne(
                      Builders<User>.Filter.Eq(x => x._id, user.SingleOrDefault()._id),
                      Builders<User>.Update.Set(x => x.favoriteCompanies, favCompanyNames),
                      new UpdateOptions { IsUpsert = true }
                     );

                apiResponse.IsSuccess = true;
                apiResponse.Message = "Company Marked/Unmarked as Favourites";
            }
            else
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Error on Mark/Unmark Company as Favourites";
            }

            return apiResponse;
        }

        /// <summary>
        /// Helper method to fetch user's favourite companies
        /// </summary>
        /// <param name="loggedInUserEmail"></param>
        /// <returns></returns>
        public ApiResponse GetFavouriteCompanies(string loggedInUserEmail)
        {
            ApiResponse apiResponse = new ApiResponse();

            List<Company> favouriteCompaniesList = new List<Company>();
            var user = UserCollection.Find(x => x.Email == loggedInUserEmail).SingleOrDefault();
            if (user.favoriteCompanies != null)
            {
                user.favoriteCompanies.ForEach(x =>
                {
                    favouriteCompaniesList.Add(CompanyCollection.Find(c => c.Name == x).SingleOrDefault());
                });

            }

            apiResponse.IsSuccess = true;
            apiResponse.Message = "Favourite Companies Retrieved Sucessfully";
            apiResponse.ReturnValue = favouriteCompaniesList;
            return apiResponse;
        }

        #endregion
    }
}