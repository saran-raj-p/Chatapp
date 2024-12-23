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
        var databaseconnection = _connectionFactory.OpenSqlConnection();

        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("id", model);

        var result = await databaseconnection.QueryFirstOrDefaultAsync<Users>("GetProfileData", parameters,commandType: CommandType.StoredProcedure);

        return result;
    }

}
