using System;
using System.Collections.Generic;
using System.Text;

namespace TestProg.ConsoleClient.Models.RequestModels
{
    public class PutUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
