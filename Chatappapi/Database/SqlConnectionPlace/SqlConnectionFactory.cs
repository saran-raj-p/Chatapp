using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;


namespace management_system_backend_api.Database.SqlConnectionPlace
{
    public class SqlConnectionFactory
    {
        private readonly string? _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }


        public IDbConnection OpenSqlConnection()
        {
            var connect = new SqlConnection(_connectionString);
            connect.Open();
            return connect;
        }

    }
}
