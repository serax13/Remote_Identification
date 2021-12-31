using Identification.Models;
using Identification.Panel.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace Identification.Panel.Services.Implementations
{
    public class ClientService : IClientService
    {
        public ILocalStorageService LocalStorageService { get; }
        private readonly HttpClient httpClient;
        public ClientService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            LocalStorageService = localStorageService;
        }
        public async Task<Client> AddClient(Client newClient)
        {
            return await httpClient.PostJsonAsync<Client>("api/clients", newClient);
        }

        public async Task<List<Client>> GetAll(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await LocalStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (responseStatusCode.ToString() == "OK")
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<Client>>(responseBody));
            }
            else
                return null;
        }

        public async Task<Client> GetById(string requestUri, int id)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri + id);

            var token = await LocalStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(JsonConvert.DeserializeObject<Client>(responseBody));
        }

        public async Task<Client> UpdateClient(string requestUri, Client newClient)
        {
            var serializedUser = JsonConvert.SerializeObject(newClient);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
            var token = await LocalStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<Client>(responseBody);

            return await Task.FromResult(returnedObj);
        }
    }
}
