using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;
using System.Data;

namespace Chatappapi.Repository;

public class ProfileRepository : IProfileRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ProfileRepository(SqlConnectionFactory dbconnection)
    {
        _connectionFactory = dbconnection;
    }

    public async Task<Users?> GetProfileData(Guid model)
    {
        var databaseConnection = _connectionFactory.OpenSqlConnection();

        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("id", model);

        var result = await databaseConnection.QueryFirstOrDefaultAsync<Users>("GetProfileData", parameters,commandType: CommandType.StoredProcedure);

        return result;
    }

    public async Task<int?> UpdateProfileData(updateProfile model)
    {
        try
        {
            
            var databaseConnection = _connectionFactory.OpenSqlConnection();

            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", model.Id);
            parameters.Add("name", model.Name);
            parameters.Add("email", model.Email);
            parameters.Add("phone", model.Phone);
            parameters.Add("profileUrl", model.ProfileUrl);

            
            var result = await databaseConnection.QueryFirstOrDefaultAsync<int?>(
                "UpdateProfileData",
                parameters,
                commandType: CommandType.StoredProcedure
            );

           
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the profile in database.",ex);
        }
    }


}
