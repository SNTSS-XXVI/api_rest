namespace SNTSS_API.DTO
{
    public class EscalafonDTO
    {
        public int IdEscalafon { get; set; }
        public DateTime DateEscalafon { get; set; }
        public DateTime DateUpdateEscalafon { get; set; }
        public double? QualificationsEscalafon { get; set; }
        public int GrupEscalafon { get; set; }
        public int? TypeHiringEscalafon { get; set; }
        public string? StatusEscalafon { get; set; }
        public int UserIdEscalafon { get; set; }
        public int NumberEscalafon { get; set; }
        public string CategoryEscalafon { get; set; } = null!;

        public virtual UsersDTO UserIdEscalafonNavigation { get; set; } = null!;
    }
}
