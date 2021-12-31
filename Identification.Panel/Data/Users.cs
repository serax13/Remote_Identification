using Identification.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identification.Panel.Data
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
        public bool RememberMe { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public short RoleUsersId { get; set; }
        public int PubId { get; set; }
        public DateTime? HireDate { get; set; }

        public RoleUsers Role { get; set; }
        public string ConfirmPassword { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ErrorMessage { get; set; }
    }
}
