using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual Login User { get; set; }
    }
}
