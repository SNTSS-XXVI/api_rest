using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Temp
    {
        public DateTime DateEscalafon { get; set; }
        public double QualificationsEscalafon { get; set; }
        public int GrupEscalafon { get; set; }
        public int TypeHiringEscalafon { get; set; }
        public string StatusEscalafon { get; set; } = null!;
        public string UserIdEscalafon { get; set; } = null!;
        public int NumberEscalafon { get; set; }
        public string CategoryEscalafon { get; set; } = null!;
        public int MatrículaScalafon { get; set; }
        public int? DayWorkedScalafon { get; set; }
        public string? Observaciones { get; set; }
    }
}
