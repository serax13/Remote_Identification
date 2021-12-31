using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class Login
    {
        public Login()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int UserId { get; set; }
        public string Login1 { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public short RoleUsersId { get; set; }
        public DateTime? HireDate { get; set; }

        public virtual RolesUser RoleUsers { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
