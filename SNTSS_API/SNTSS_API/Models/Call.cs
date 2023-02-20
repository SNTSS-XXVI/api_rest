using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Call
    {
        public int IdCalls { get; set; }
        public string TextCalls { get; set; } = null!;
        public string PdfCalls { get; set; } = null!;
        public string DateCreateCalls { get; set; } = null!;
        public string DateFinallyCalls { get; set; } = null!;
    }
}
