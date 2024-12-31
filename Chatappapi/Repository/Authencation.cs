
using Chatappapi.Interface;
using Chatappapi.Model;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

using System.Threading.Tasks; // For async/await




namespace Chatappapi.Repository
{
    public class Authencation : IAuthentication
    {
        private readonly SqlConnectionFactory _databaseconnection;
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly IEmailService _emailService;
        public Authencation(SqlConnectionFactory db)
        {
            _databaseconnection = db;
            _passwordHasher = new PasswordHasher<object>();
            
        }

        

        
        public async Task<int> UserData(LoginDTo login)
        {
            try
            {
                var db = _databaseconnection.OpenSqlConnection();
                var email = login.Email;
                var password = login.Password;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);
                parameters.Add("password", password);
                var result = await db.QueryFirstOrDefaultAsync<int>("CheckUserExist", parameters, commandType: CommandType.StoredProcedure);
                if (result == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
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
                parameters.Add("uid", guid);
                parameters.Add("name", name);
                parameters.Add("password", password);
                parameters.Add("email", email);
                parameters.Add("phone", phone);
                parameters.Add("otp", otp);
                var result = await db.QueryFirstAsync<LoginDTo>("UserRegistration", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public String otpGenerate()
        {
            Random rand = new Random();
            var otp = rand.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            try
            {
                var db = _databaseconnection.OpenSqlConnection();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);

                var result = await db.QueryFirstOrDefaultAsync<int>("CheckEmailExists", parameters, commandType: CommandType.StoredProcedure);
                return result == 1;
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }


        public async Task<bool> SendOtpAsync(string email)
        {
            try
            {
                var otp = otpGenerate();

                var db = _databaseconnection.OpenSqlConnection();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);
                parameters.Add("otp", otp);
              //  parameters.Add("otpExpiry", expiration);

                var result = await db.ExecuteAsync("SaveOtp", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    await SendEmailAsync(email, "Your OTP Code", $"Your OTP is {otp}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }


        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            try
            {
                var db = _databaseconnection.OpenSqlConnection();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);
                parameters.Add("otp", otp);

                var result = await db.QueryFirstOrDefaultAsync<int>("VerifyOtp", parameters, commandType: CommandType.StoredProcedure);

                return result == 1; // 1 indicates OTP is valid
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // Logic for sending email, e.g., using SMTP, SendGrid, etc.
            // Make sure to send the email asynchronously.

            // For example, using an SMTP client:
            var smtpClient = new SmtpClient();
            var message = new MailMessage("from@example.com", to, subject, body);
            await smtpClient.SendMailAsync(message);
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            try
            {
               // var hashedPassword = _passwordHasher.HashPassword(newPassword);

                var db = _databaseconnection.OpenSqlConnection();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("email", email);
                parameters.Add("newPassword", newPassword);

                var result = await db.ExecuteAsync("ResetPassword", parameters, commandType: CommandType.StoredProcedure);

                return result > 0; // Success if rows affected > 0
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }


        }

        public class EmailService : IEmailService
        {
            public async Task SendEmailAsync(string to, string subject, string body)
            {
                // Logic for sending email, e.g., using SMTP, SendGrid, etc.
                // Make sure to send the email asynchronously.

                // For example, using an SMTP client:
                var smtpClient = new SmtpClient();
                var message = new MailMessage("from@example.com", to, subject, body);
                await smtpClient.SendMailAsync(message);
            }
        }



    }
}

    



