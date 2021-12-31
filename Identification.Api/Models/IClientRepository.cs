using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identification.Models;

namespace Identification.Api.Models
{
    public interface IClientRepository
    {
        Task<Client> AddClient(Client cc);
        Task<List<Client>> GetAll();
        Task<Client> GetFromDb(int clId);
        Task<Client> UpdateClient(Client cc);
        Task<Client> DeleteClient(int clId);
    }
}
