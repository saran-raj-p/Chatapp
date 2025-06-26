using Chatappapi.Model;
namespace Chatappapi.Interface
{
    public interface IChat
    {
        public Task<Boolean> sendMessage(Guid fromId, Guid toId,  String Message);

    }
}
