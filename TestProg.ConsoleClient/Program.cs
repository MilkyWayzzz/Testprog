using ConsoleClient.Services;
using System;
using TestProg.ConsoleClient.Models.RequestModels;
using TestProg.ConsoleClient.Models.ResponseModels;

namespace ConsoleClient
{
    class Program
    {
        static HttpService _httpService = new HttpService();

        static void Main(string[] args)
        {           
            while (true)
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("create user");
                Console.WriteLine("update user");
                Console.WriteLine("delete user");
                Console.WriteLine("get user");
                Console.WriteLine("get users");
                var userstring = Console.ReadLine().ToLower();
                switch (userstring)
                {
                    case "get users":
                        {
                            var users = _httpService.GetUsersAsync().Result;

                            if(users == null)
                            {
                                Console.WriteLine("Service error");
                                break;
                            }
                                
                            if(users.Count == 0)
                            {
                                Console.WriteLine("No users");
                                break;
                            }

                            foreach (var item in users)
                            {
                                Console.WriteLine(item.Id + "  " + item.FirstName + "  " + item.LastName + "  " + item.Age + "  " + item.Email);
                            }
                        }
                        break;
                    case "get user":
                        {
                            Console.WriteLine("Enter user ID");
                            var id = Convert.ToInt32(Console.ReadLine());
                            var user = _httpService.GetUserAsync(id).Result;

                            if (user == null)
                            {
                                Console.WriteLine("Service error");
                                break;
                            }
                                
                            Console.WriteLine(user.Id + "  " + user.FirstName + "  " + user.LastName + "  " + user.Age + "  " + user.Email);
                        }
                        break;
                    case "create user":
                        {
                            Console.WriteLine("Enter Name");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter Last Name");
                            var lastname = Console.ReadLine();
                            Console.WriteLine("Enter Age");
                            var age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Mail");
                            var mail = Console.ReadLine();
                            var user = new PostUserRequest() { FirstName = name, LastName = lastname, Age = age, Email = mail };

                            var isSuccess = _httpService.CreateUserAsync(user).Result;

                            if (!isSuccess)
                            {
                                Console.WriteLine("Service error");
                                break;
                            }

                            Console.WriteLine("User successfully added");
                        }
                        break;
                    case "update user":
                        {
                            Console.WriteLine("Enter ID");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Name");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter Last Name");
                            var lastname = Console.ReadLine();
                            Console.WriteLine("Enter Age");
                            var age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Mail");
                            var mail = Console.ReadLine();
                            var user = new PutUserRequest() {FirstName = name, LastName = lastname, Age = age, Email = mail };

                            var isSuccess = _httpService.UpdateUserAsync(id, user).Result;

                            if (!isSuccess)
                            {
                                Console.WriteLine("Service error");
                                break;
                            }

                            Console.WriteLine("User successfully updated");
                        }
                        break;
                    case "delete user":
                        {
                            Console.WriteLine("Enter user ID");
                            var id = Convert.ToInt32(Console.ReadLine());

                            var isSuccess = _httpService.DeleteUserAsync(id).Result;

                            if (!isSuccess)
                            {
                                Console.WriteLine("Service error");
                                break;
                            }

                            Console.WriteLine("User successfully deleted");
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong enter");
                        break;
                }
            }
        }
    }
}
