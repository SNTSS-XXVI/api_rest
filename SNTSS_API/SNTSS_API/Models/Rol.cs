using System;
using System.Collections.Generic;

namespace SNTSS_API.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int IdRol { get; set; }
        public string NameRol { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
