using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QUETE_APIRestNANCY
{
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            Get("/users/{UserId:int}", parameter => ReturnUserData(parameter.UserId));           
            Get("/delete/users/{UserId:int}", parameters => DeleteUser(parameters.UserId));
            Get("/put/users/{Name}/{Password}", parameters => PutNewUser(parameters.Name, parameters.Password));
            Get("/post/authentify/{UserId}&{Password}", parameters => Authentification(parameters.UserId, parameters.Password));
        }

        public dynamic ReturnUserData(int userId)
        {
            using (var context = new UserContext())
            {
                var user = from u in context.Users
                           where u.UserId == userId
                           select u;
                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize(user);
                return jsonString;
            }            
        }

        public dynamic DeleteUser(int userId)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.Where(c => c.UserId == userId).First();
                context.Users.Remove(user);
                context.SaveChanges();

                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize(user);
                return jsonString + " is deleted";
            }
        }

        public dynamic PutNewUser(string name, string password)
        {
            using (var context = new UserContext())
            {
                var user = new User();
                user.Name = name;
                user.Password = password;
                context.Users.AddRange(user);
                context.SaveChanges();

                string jsonString;
                jsonString = System.Text.Json.JsonSerializer.Serialize("User added.");
                return jsonString;
            }
        }

        public dynamic Authentification(int userId, string password)
        {
            using (var context = new UserContext())
            {
                var query = (from u in context.Users
                            where u.UserId == userId && u.Password == password
                            select u).FirstOrDefault();
                string jsonString;
                var user = new User();
                if(query != null)
                {
                    jsonString = System.Text.Json.JsonSerializer.Serialize("Authentification ok");
                }
                else
                {
                    jsonString = System.Text.Json.JsonSerializer.Serialize("Error");
                }              
                return jsonString;
            }
        }
    }

    
}
