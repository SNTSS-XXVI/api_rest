namespace SNTSS_API.DTO
{
    public class LogsDTO
    {
        public DateTime FechaLogs { get; set; } = DateTime.Now;
        public string TypeLogs { get; set; } = null!;
        public string UserLogs { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
