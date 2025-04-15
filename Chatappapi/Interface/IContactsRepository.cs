using Chatappapi.Model;

namespace Chatappapi.Interface
{
    public interface IContactsRepository
    {
        public Task<Contacts> createcontact(Contacts contact);

        public Task<Contacts> getcontact(Contacts contact);
    }
}
