using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Convention
    {
        public int IdConventions { get; set; }
        public string DateCreateConventions { get; set; } = null!;
        public string PictureConventions { get; set; } = null!;
        public string TitleConventions { get; set; } = null!;
        public string TypeConventions { get; set; } = null!;
    }
}
