using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identification.Models;

namespace Identification.Panel.Services.Contracts
{
    public interface IClientService
    {
        Task<Client> AddClient(Client newClient);
        Task<List<Client>> GetAll(string requestUri);
        Task<Client> GetById(string requestUri, int id);
        Task<Client> UpdateClient(string requestUri, Client newClient);
    }
}
