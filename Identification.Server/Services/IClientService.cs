using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identification.Models;

namespace Identification.Server.Services
{
    public interface IClientService
    {
        Task<Client> AddClient(Client newClient);

    }
}
