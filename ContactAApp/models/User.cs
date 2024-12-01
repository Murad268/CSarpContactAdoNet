using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAdoNet.models
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User() { }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"ID: {Id}, Username: {Username}");
        }
    }
}

