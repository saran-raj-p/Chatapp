using Chatappapi.Model;

namespace Chatappapi.Interface
{
    public interface IContactsRepository
    {
        public Task<Contacts> createcontact(ContactsDTO contact);

        public Task<IEnumerable<Contacts>>getcontact(getContact contact);
    }
}
