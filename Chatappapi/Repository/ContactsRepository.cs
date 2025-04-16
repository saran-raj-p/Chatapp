using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Chatappapi.Interface;
using Chatappapi.Model;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;


namespace Chatappapi.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly SqlConnectionFactory _sqlConnnectionFactory;
        public ContactsRepository(SqlConnectionFactory sqlConnnectionFactory)
        {
            _sqlConnnectionFactory = sqlConnnectionFactory;
        }
        public async Task<Contacts> createcontact(Contacts contact) {
            try
            {
                using var database = _sqlConnnectionFactory.OpenSqlConnection();

                DynamicParameters parameters = new DynamicParameters();


                parameters.Add("name", contact.name);
                parameters.Add("email", contact.email);
                parameters.Add("phone", contact.phone);
                parameters.Add("userId", contact.userId);

                var result = await database.QueryFirstOrDefaultAsync<Contacts>("CreateContact", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch(SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<IEnumerable<Contacts>> getcontact(Contacts contact)
        {
            try
            {
                using var database = _sqlConnnectionFactory.OpenSqlConnection();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("userId", contact.userId);

                var result = database.QueryAsync<Contacts>("GetContact", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
