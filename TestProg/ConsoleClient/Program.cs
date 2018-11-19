using Domain;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpService service = new HttpService();
            

            while (true)
            {
                Console.WriteLine("Change event: add, update, delete, viewuser, viewuserslist");
                var userstring = Console.ReadLine().ToLower();
                switch (userstring)
                {
                    case "viewuserslist":
                        {
                            var listusers = service.GetListUser().Result;
                            foreach (var item in listusers)
                            {
                                Console.WriteLine(item.Id + "  " + item.FirstName + "  " + item.LastName + "  " + item.Age + "  " + item.Email);
                            }
                        }
                        break;
                    case "viewuser":
                        {
                            Console.WriteLine("Enter user ID");
                            var id = Convert.ToInt32(Console.ReadLine());
                            var user = service.GetUser(id).Result;
                            Console.WriteLine(user.Id + "  " + user.FirstName + "  " + user.LastName + "  " + user.Age + "  " + user.Email);
                        }
                        break;
                    case "add":
                        {

                            Console.WriteLine("Enter Name");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter Last Name");
                            var lastname = Console.ReadLine();
                            Console.WriteLine("Enter Age");
                            var age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Mail");
                            var mail = Console.ReadLine();
                            User user = new User() { FirstName = name, LastName = lastname, Age = age, Email = mail };
                            service.Create(user);
                            Console.WriteLine("User success added");
                        }
                        break;
                    case "update":
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
                            User user = new User() {FirstName = name, LastName = lastname, Age = age, Email = mail };
                            service.Update( id, user);
                            Console.WriteLine("User success updated");
                        }
                        break;
                    case "delete":
                        {
                            Console.WriteLine("Enter user ID");
                            var id = Convert.ToInt32(Console.ReadLine());
                            service.Delete(id);
                            Console.WriteLine("User success deleted");
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
