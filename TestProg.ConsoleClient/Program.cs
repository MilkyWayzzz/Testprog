using ConsoleClient.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestProg.ConsoleClient.Exceptions;
using TestProg.ConsoleClient.Models.RequestModels;
using TestProg.ConsoleClient.Models.ResponseModels;

namespace ConsoleClient
{
    class Program
    {
        static HttpService _httpService = new HttpService();

        static void Main(string[] args)
        {
            ShowStartScreen();

            while (true)
            {
                var input = Console.ReadLine();

                int command = 0;
                bool commandParsed = int.TryParse(input, out command);

                if(!commandParsed || command < 1 || command > 5)
                {
                    Console.WriteLine("No such command. Please input command number [1-5]:");
                    continue;
                }

                try
                {
                    switch (command)
                    {
                        case 1: RunGetUsersCommandAsync().GetAwaiter().GetResult(); break;
                        case 2: RunGetUserCommandAsync().GetAwaiter().GetResult(); break;
                        case 3: RunCreateUserCommandAsync().GetAwaiter().GetResult(); break;
                        case 4: RunUpdateUserCommandAsync().GetAwaiter().GetResult(); break;
                        case 5: RunDeleteUserCommandAsync().GetAwaiter().GetResult(); break;
                    }
                }
                catch (HttpServiceException ex)
                {
                    Console.WriteLine(ex.Message);                    
                }  
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Service error");
                }
                catch
                {
                    Console.WriteLine("Unknown error");
                }
            }
        }

        static void ShowStartScreen()
        {
            Console.WriteLine(" C# Console Application TestProg");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine("1. Get users");
            Console.WriteLine("2. Get user");
            Console.WriteLine("3. Create user");
            Console.WriteLine("4. Update user");
            Console.WriteLine("5. Delete user");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Please input command number [1-5]:");
        }

        static async Task RunGetUsersCommandAsync()
        {
            Console.WriteLine("Loading...");

            var users = await _httpService.GetUsersAsync();

            if (users.Count == 0)
            {
                Console.WriteLine("No users");
                return;
            }

            Console.WriteLine("ID     | First name         | Last name         | Age   | Email                 ");
            Console.WriteLine("=======|====================|===================|=======|=======================");

            foreach (var user in users)
            {
                string id = GetFormattedValue(user.Id.ToString(), 7);
                string firstName = GetFormattedValue(user.FirstName, 19);
                string lastName = GetFormattedValue(user.LastName, 18);
                string age = GetFormattedValue(user.Age.ToString(), 6);
                string email = GetFormattedValue(user.Email, 22);

                Console.WriteLine(id + "| " + firstName + "| " + lastName + "| " + age + "| " + email);
            }
        }

        static async Task RunGetUserCommandAsync()
        {
            Console.WriteLine("ID:");
            var id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Loading...");

            var user = await _httpService.GetUserAsync(id);

            Console.WriteLine(" Full user profile");
            Console.WriteLine("===================");
            Console.WriteLine("ID: " + user.Id);
            Console.WriteLine("First name: " + user.FirstName);
            Console.WriteLine("Last name:" + user.LastName);
            Console.WriteLine("Age:" + user.Age);
            Console.WriteLine("Email:" + user.Email);            
        }

        static async Task RunCreateUserCommandAsync()
        {
            Console.WriteLine("Fill in the details of the new user");    

            Console.WriteLine("First name:");
            var name = Console.ReadLine();

            Console.WriteLine("Last name:");
            var lastName = Console.ReadLine();

            Console.WriteLine("Age:");
            var age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Email:");
            var email = Console.ReadLine();

            var user = new PostUserRequest() { FirstName = name, LastName = lastName, Age = age, Email = email };

            Console.WriteLine("Loading...");

            await _httpService.CreateUserAsync(user);
          
            Console.WriteLine("User successfully created");
        }

        static async Task RunUpdateUserCommandAsync()
        {
            Console.WriteLine("Fill in new user details");

            Console.WriteLine("ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("First name:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Last name:");
            var lastName = Console.ReadLine();

            Console.WriteLine("Age:");
            var age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Email:");
            var email = Console.ReadLine();

            var user = new PutUserRequest() { FirstName = firstName, LastName = lastName, Age = age, Email = email };

            Console.WriteLine("Loading...");

            await _httpService.UpdateUserAsync(id, user);
      
            Console.WriteLine("User successfully updated");
        }

        static async Task RunDeleteUserCommandAsync()
        {
            Console.WriteLine("Enter user ID");
            var id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Loading...");

            await _httpService.DeleteUserAsync(id);
           
            Console.WriteLine("User successfully deleted");
        }

        static string GetFormattedValue(string value, int columnSize)
        {
            int whiteSpacesSize = columnSize - value.Length;

            string whiteSpaces = "";

            for (int i = 0; i < whiteSpacesSize; i++)
            {
                whiteSpaces = whiteSpaces + " ";
            }

            if (value.Length > columnSize)
                value = value.Substring(0, columnSize - 3) + "...";

            return whiteSpacesSize > 0 ? value + whiteSpaces : value;
        }        
    }
}
