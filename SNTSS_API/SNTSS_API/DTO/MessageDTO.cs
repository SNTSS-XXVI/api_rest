namespace SNTSS_API.DTO
{
    public class MessageDTO
    {
        public int IdMessageT { get; set; }
        public string ContenidoMessageT { get; set; } = null!;
        public DateTime DateMessageT { get; set; }
        public string TitleMessageT { get; set; } = null!;


        public MessageDTO(string _title, DateTime _dateTime, string _message)
        {
            this.TitleMessageT = _title;
            this.DateMessageT = _dateTime;
            this.ContenidoMessageT = _message;
        }
    }
    public class PostMessage
    {
        public string menssage { get; set; }
        public int iduser { get; set; }
        public string title { get; set; }
    }
}
