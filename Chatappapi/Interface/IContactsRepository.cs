using Chatappapi.Model;

namespace Chatappapi.Interface
{
    public interface IContactsRepository
    {
        public Task<Contacts> createcontact(Contacts contact);

        public Task<IEnumerable<Contacts>>getcontact(Contacts contact);
    }
}
