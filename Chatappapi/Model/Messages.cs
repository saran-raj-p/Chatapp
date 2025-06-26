namespace Chatappapi.Model
{
    public class Messages
    {
        public Guid fromId { get; set; }
        public Guid toId { get; set; }
        public DateTime sendTime {  get; set; }
        public string message { get; set; }
        public int readStatus { get; set; }
    }

}
