using System.Data;
using Chatappapi.Interface;
using Dapper;
using management_system_backend_api.Database.SqlConnectionPlace;

namespace Chatappapi.Repository
{
    public class ChatRepository : IChat
    {
        public readonly SqlConnectionFactory _ConnectionFactory;
        public ChatRepository(SqlConnectionFactory db)
        {
            _ConnectionFactory = db;
        }
        async Task<bool> IChat.sendMessage(Guid fromId, Guid toId,  string Message)
        {
            try
            {
                var db = _ConnectionFactory.OpenSqlConnection();
                if (fromId == null || toId == null || Message == null)
                {
                    return false;
                }
                else
                {
                    var from = fromId;
                    var to = toId;
                    var date = DateTime.Now;
                    var message = Message;
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("FromId", from);
                    parameters.Add("ToId", to);
                    parameters.Add("SendTime", date);
                    parameters.Add("Message", Message);
                    var result = db.QueryFirstAsync<bool>("SendMessage", parameters, commandType: CommandType.StoredProcedure);
                    return result != null;
                }
            }
            catch (Exception ex) { 
                return false;
            }

            
            
        }
    }
}
