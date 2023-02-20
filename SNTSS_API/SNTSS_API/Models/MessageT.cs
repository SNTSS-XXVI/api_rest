using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class MessageT
    {
        public MessageT()
        {
            MessageHasUsers = new HashSet<MessageHasUser>();
        }

        public int IdMessageT { get; set; }
        public string ContenidoMessageT { get; set; } = null!;
        public DateTime DateMessageT { get; set; }
        public string TitleMessageT { get; set; } = null!;

        public virtual ICollection<MessageHasUser> MessageHasUsers { get; set; }
    }
}
