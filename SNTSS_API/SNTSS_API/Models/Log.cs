using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Log
    {
        public DateTime FechaLogs { get; set; }
        public string TypeLogs { get; set; } = null!;
        public string UserLogs { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
