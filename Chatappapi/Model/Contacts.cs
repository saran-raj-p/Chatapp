namespace Chatappapi.Model
{
    public class Contacts
    {
        public Guid id { get; set;}

        public String name { get; set; }

        public String email { get; set; }

        public String phone { get; set; }

        public Guid userId { get; set; }

    }

    public class ContactsDTO
    {

        public String name { get; set; }

        public String email { get; set; }

        public String phone { get; set; }

        public Guid userId { get; set; }

    }

    public class getContact
    {

        public Guid userId { get; set; }

    }
}
