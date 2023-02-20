namespace SNTSS_API.DTO
{
    public class CallsDTO
    {
        public int IdCalls { get; set; }
        public string TextCalls { get; set; } = null!;
        public string PdfCalls { get; set; } = null!;
        public string DateCreateCalls { get; set; } = null!;
        public string DateFinallyCalls { get; set; } = null!;
    }
}
