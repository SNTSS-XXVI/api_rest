using System.Reflection;

namespace SNTSS_API.DTO
{
    public class MessageRelationship
    {
        public string matricula { get; set; }
        public string title { get; set; }
        public List<MessageDTO> message { get; set; }

        public MessageRelationship() { }

        public MessageRelationship(string _matricula, string _title, List<MessageDTO> _message)
        {
            this.matricula = _matricula;
            this.title = _title;
            this.message = _message;
        }
    }
}
