using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identification.Models
{
    public class RoleUsers
    {
        public RoleUsers()
        {
            Users = new HashSet<User>();
        }
        public short RoleUsersId { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
