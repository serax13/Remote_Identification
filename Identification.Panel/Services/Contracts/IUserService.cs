using Identification.Panel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identification.Panel.Services.Contracts
{
    public interface IUserService
    {
        public Task<Users> LoginAsync(Users user);
        public Task<Users> RegisterUserAsync(Users user);
        public Task<Users> GetUserByAccessTokenAsync(string accessToken);
        public Task<Users> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
