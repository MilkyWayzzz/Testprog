using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleClient
{
    public class HttpService
    {
        //Users CRUD GetUserData GetListUsers  
        List<User> users = new List<User>();

        public  User GetUser (int id )
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            return user;
        }
        public List<User> GetListUser()
        {
            return users;
        }
        public void Create(User user)
        {
            users.Add(user);
            
        }
        public void Update(User user)
        {
            var userfind = users.FirstOrDefault(x => x.ID == user.ID);
            userfind.Age = user.Age;
            userfind.First_Name = user.First_Name;
            userfind.Last_Name = user.Last_Name;
            userfind.Mail = user.Mail;
        }
        public void Delete(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            users.Remove(user);
        }
    }
}
