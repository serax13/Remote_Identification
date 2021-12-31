using Identification.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identification.Server.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient httpClient;
        public ClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient; 
        }
        public async Task<Client> AddClient(Client newClient)
        {
            return await httpClient.PostJsonAsync<Client>("api/clients", newClient);
        }
    }
}
