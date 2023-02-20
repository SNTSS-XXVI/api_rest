using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class MessageHasUser
    {
        public int IdMessageHasUser { get; set; }
        public int UserMessageHasUser { get; set; }
        public int MessageMessageHasUser { get; set; }

        public virtual MessageT MessageMessageHasUserNavigation { get; set; } = null!;
        public virtual User UserMessageHasUserNavigation { get; set; } = null!;
    }
}
