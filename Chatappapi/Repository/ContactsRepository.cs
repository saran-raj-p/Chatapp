using System.Data;
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
        public Task<Contacts> createcontact(Contacts contact) {
            var database = _sqlConnnectionFactory.OpenSqlConnection();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("id",contact.id);
            parameters.Add("name", contact.name);
            parameters.Add("email", contact.email);
            parameters.Add("phone", contact.phone);
            parameters.Add("userId", contact.userId);

            var result = database.QueryFirstOrDefaultAsync<Contacts>("CreateContact",parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public Task<Contacts> getcontact(Contacts contact)
        {
            var database = _sqlConnnectionFactory.OpenSqlConnection();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("userId", contact.userId);

            var result = database.QueryFirstOrDefaultAsync<Contacts>("GetContact", parameters, commandType: CommandType.StoredProcedure);

            return result;

        }
    }
}
