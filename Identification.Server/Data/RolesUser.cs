using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class RolesUser
    {
        public RolesUser()
        {
            Logins = new HashSet<Login>();
        }

        public short RoleUsersId { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
