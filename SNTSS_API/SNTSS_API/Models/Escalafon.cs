using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Escalafon
    {
        public int IdEscalafon { get; set; }
        public DateTime DateEscalafon { get; set; }
        public DateTime DateUpdateEscalafon { get; set; }
        public double? QualificationsEscalafon { get; set; }
        public int GrupEscalafon { get; set; }
        public int? TypeHiringEscalafon { get; set; }
        public string? StatusEscalafon { get; set; }
        public int NumberEscalafon { get; set; }
        public int? DayWorkedScalafon { get; set; }
        public int? Matricula { get; set; }
        public string CategoryEscalafon { get; set; } = null!;
        public string? Observaciones { get; set; }
    }
}
