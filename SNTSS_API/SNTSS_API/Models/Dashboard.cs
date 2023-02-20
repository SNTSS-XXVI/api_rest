using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Dashboard
    {
        public int IdDashboard { get; set; }
        public string DateDashboard { get; set; } = null!;
        public string NameDashboard { get; set; } = null!;
    }
}
