using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class HttpService
    {
        //Users CRUD GetUserData GetListUsers
        HttpClient client = new HttpClient();
        List<User> users = new List<User>();

        public async Task<User> GetUser (int id )
        {
            var response = await client.GetAsync("https://localhost:44338/api/users/"+id);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(json);
        }
        public async Task<List<User>> GetListUser()
        {
            var response = await client.GetAsync("https://localhost:44338/api/users");
            var json = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
        public async Task Create(User user)
        {
            var userjson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userjson, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:44338/api/users",content);
            
        }
        public async Task Update(int id , User user)
        {
            var userjson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userjson, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:44338/api/users/" + id, content);
        }
        public async Task Delete(int id)
        {
            await client.DeleteAsync("https://localhost:44338/api/users/" + id);
        }
    }
}
