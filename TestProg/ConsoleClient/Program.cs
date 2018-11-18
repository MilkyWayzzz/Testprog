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
                            var listusers = service.GetListUser();
                            foreach (var item in listusers)
                            {
                                Console.WriteLine(item.First_Name);
                                Console.WriteLine(item.Last_Name);
                                Console.WriteLine(item.Age);
                                Console.WriteLine(item.Mail);
                            }
                        }
                        break;
                    case "viewuser":
                        {
                            Console.WriteLine("Enter user ID");
                            var id = Convert.ToInt32(Console.ReadLine());
                            var user = service.GetUser(id);
                            Console.WriteLine(user.First_Name);
                            Console.WriteLine(user.Last_Name);
                            Console.WriteLine(user.Age);
                            Console.WriteLine(user.Mail);
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
                            User user = new User() { First_Name = name, Last_Name = lastname, Age = age, Mail = mail };
                            service.Create(user);
                            Console.WriteLine("User success added");
                        }
                        break;
                    case "update":
                        {
                            Console.WriteLine("Enter Name");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter Last Name");
                            var lastname = Console.ReadLine();
                            Console.WriteLine("Enter Age");
                            var age = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Mail");
                            var mail = Console.ReadLine();
                            User user = new User() { First_Name = name, Last_Name = lastname, Age = age, Mail = mail };
                            service.Update(user);
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
