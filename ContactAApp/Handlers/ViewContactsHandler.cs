using System;
using System.Collections.Generic;
using ContactAApp.Services;

namespace ContactAApp.Handlers
{
    internal class ViewContactsHandler
    {
        private readonly ContactService _contactService;

        public ViewContactsHandler(ContactService contactService)
        {
            _contactService = contactService;
        }

        public bool Handle(int userId)
        {
            var contacts = _contactService.GetContacts(userId);

            if (contacts.Count == 0)
            {
                Console.WriteLine("Your contact list is empty.");
                return false;
            }

            Console.WriteLine("Your Contacts:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.Phone}");
            }

            return true;
        }
    }
}
