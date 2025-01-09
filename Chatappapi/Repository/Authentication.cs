
using Chatappapi.Interface;
using Chatappapi.Model;
using Chatappapi.services;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.ObjectPool;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace Chatappapi.Repository
{
    public class Authentication : IAuthentication
    {
        private readonly SqlConnectionFactory _databaseconnection;
        public Authentication(SqlConnectionFactory db)
        {
            _databaseconnection = db;
        }
        public async Task<LoginDTo> UserData(LoginDTo login)
        {
            if(login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                throw new ArgumentNullException("Invalid Data");
            }
            try
            {
                
                using var db = _databaseconnection.OpenSqlConnection();
                var email = login.Email;
                var password = login.Password;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);
                parameters.Add("password", password);
                var result =  await db.QueryFirstOrDefaultAsync<LoginDTo>("CheckUserExist", parameters, commandType: CommandType.StoredProcedure);
                
                return result;
                
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        public async Task<LoginDTo> UserRegister(RegisterDto userdata)
        {
            try
            {
                var db = _databaseconnection.OpenSqlConnection();
                var guid = Guid.NewGuid();
                var name = userdata.Name;
                var password = userdata.Password;
                var email = userdata.Email;
                var phone = userdata.Phone;
                var otp = otpGenerate();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("uid",guid);
                parameters.Add("name", name);
                parameters.Add("password", password);
                parameters.Add("email", email);
                parameters.Add("phone",phone);
                parameters.Add("otp",otp);
                var result = await db.QueryFirstAsync<LoginDTo>("UserRegistration",parameters,commandType:CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public  string saveRefreshToken(String refreshtoken,Guid id)
        {
            try
            {
                var db = _databaseconnection.OpenSqlConnection();
                var uid = id;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("uid",uid);
                parameters.Add("refreshtoken",refreshtoken);
                var result =  db.QueryFirst("saveRefreshToken",parameters,commandType:CommandType.StoredProcedure);
                if(result == 1)
                {
                    return refreshtoken;
                }
                return null;
            }
            catch { return null; }
        }
        public String otpGenerate()
        {
            Random rand = new Random();
            var otp = rand.Next(100000,999999);
            return otp.ToString();
        }
        public async Task<Getotp?> UserActivation(Getotp activation)
        {
            var db = _databaseconnection.OpenSqlConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@email", activation.Email);
            parameters.Add("@otp", activation.otp);

            try
            {

                await db.ExecuteAsync("UserActivation", parameters, commandType: CommandType.StoredProcedure);
                return new Getotp { Email = activation.Email, otp = activation.otp };
            }
            catch (Exception ex)
            {
                throw new Exception("User activation failed: ");
            }
        }
        public async Task<bool> validateRefreshToken(String token)
        {
            var db = _databaseconnection.OpenSqlConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("token", token);
            try
            {
                var result = await db.QueryFirstAsync<bool>("TokenValidation", parameters, commandType: CommandType.StoredProcedure);
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<ForgotPasswordResponse> ProcessForgotPassword(string email)
        {
            try
            {
                using var db = _databaseconnection.OpenSqlConnection();
                var parameters = new DynamicParameters();
                parameters.Add("email", email.Trim().ToLower());

                var user = await db.QueryFirstOrDefaultAsync<LoginDTo>(
                    "GetUserByEmail",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (user == null)
                {
                    return new ForgotPasswordResponse { IsEmailExists = false };
                }

                var otp = otpGenerate();
                parameters.Add("otp", otp);

                var result = await db.ExecuteAsync(
                    "UpdateForgotPasswordOTP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result > 0)
                {
                    return new ForgotPasswordResponse
                    {
                        IsEmailExists = true,
                        Email = email,
                        OTP = otp
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> VerifyOTP(string email, string otp)
        {
            try
            {
                using var db = _databaseconnection.OpenSqlConnection();
                var parameters = new DynamicParameters();
                parameters.Add("email", email.Trim().ToLower());
                parameters.Add("otp", otp);

                var isValid = await db.QueryFirstOrDefaultAsync<bool>(
                    "VerifyOTP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return isValid;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> UpdatePassword(string email, string newPassword)
        {
            try
            {
                using var db = _databaseconnection.OpenSqlConnection();
                var parameters = new DynamicParameters();
                parameters.Add("email", email.Trim().ToLower());
                parameters.Add("password", newPassword);

                var result = await db.ExecuteAsync(
                    "UpdatePassword",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }








    }
}
