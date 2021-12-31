using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identification.Models
{
    public class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public short RoleUsersId { get; set; }
        public DateTime? HireDate { get; set; }
        public virtual RoleUsers Role { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
