using System;
using System.Collections.Generic;
using System.Text;

namespace TestProg.ConsoleClient.Models.ResponseModels
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
