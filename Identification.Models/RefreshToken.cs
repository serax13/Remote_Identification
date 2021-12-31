using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identification.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }
    }
}
