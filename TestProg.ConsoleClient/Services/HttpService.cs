using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProg.ConsoleClient.Exceptions;
using TestProg.ConsoleClient.Models.RequestModels;
using TestProg.ConsoleClient.Models.ResponseModels;

namespace ConsoleClient.Services
{
    public class HttpService
    {
        private readonly HttpClient _client;
        private readonly string baseUserAPIURL = "https://localhost:44338/api/users";

        public HttpService()
        {
            _client = new HttpClient();
        }

        public async Task<GetUserResponse> GetUserAsync(int id)
        {
            var response = await _client.GetAsync($"{baseUserAPIURL}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new HttpServiceException("Service error");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GetUserResponse>(json);
        }

        public async Task<List<GetUserResponse>> GetUsersAsync()
        {
            var response = await _client.GetAsync(baseUserAPIURL);

            if (!response.IsSuccessStatusCode)
                throw new HttpServiceException("Service error");

            var json = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<GetUserResponse>>(json);
        }

        public async Task CreateUserAsync(PostUserRequest user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(baseUserAPIURL, content);

            if (!response.IsSuccessStatusCode)
                throw new HttpServiceException("Service error");
        }

        public async Task UpdateUserAsync(int id, PutUserRequest user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"{baseUserAPIURL}/{id}", content);

            if (!response.IsSuccessStatusCode)
                throw new HttpServiceException("Service error");
        }

        public async Task DeleteUserAsync(int id)
        {
            var response = await _client.DeleteAsync($"{baseUserAPIURL}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new HttpServiceException("Service error");
        }
    }
}
