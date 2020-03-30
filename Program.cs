using System;
using System.Collections.Generic;
using Nancy.Hosting.Self;

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
                    new User { Login = "Jane", Password = "1234" },
                    new User { Login = "John", Password = "4567"}
                };

                context.AddRange(userList);
                context.SaveChanges();


            }
        }
    }
}
