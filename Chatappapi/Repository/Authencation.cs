
using Chatappapi.Interface;
using Chatappapi.Model;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Chatappapi.Repository
{
    public class Authencation : IAuthentication
    {
        private readonly SqlConnectionFactory _databaseconnection;
        public Authencation(SqlConnectionFactory db)
        {
            _databaseconnection = db;
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
                var result =  await db.QueryFirstOrDefaultAsync<int>("CheckUserExist", parameters, commandType: CommandType.StoredProcedure);
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
                var name = userdata.Name;
                var password = userdata.Password;
                var email = userdata.Email;
                var phone = userdata.Phone;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", name);
                parameters.Add("password", password);
                parameters.Add("email", email);
                parameters.Add("phone",phone);
                var result = await db.QueryFirstAsync<LoginDTo>("UserRegistration",parameters,commandType:CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        

        
    }
}
