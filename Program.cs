using System;
using System.Collections.Generic;
using Nancy.Hosting.Self;
using Nancy;

namespace QUETE_APIRestNANCY
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UserContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<User> userList = new List<User>()
                {
                    new User { Name = "Jane", Password = "1234" },
                    new User { Name = "John", Password = "4567"}
                };

                context.AddRange(userList);
                context.SaveChanges();
            }

            // ligne de commande en administrateur
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }
        }
    }
}
