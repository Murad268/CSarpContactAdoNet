using System;

namespace ContactAdoNet.models
{
    internal class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(int userId, string name, string phone)
        {
            UserId = userId;
            Name = name;
            Phone = phone;
        }

        
        public Contact() { }


        public void DisplayContactInfo()
        {
            Console.WriteLine($"ID: {Id}, UserId: {UserId}, Name: {Name}, Phone: {Phone}");
        }
    }
}
