using System;
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

        public void Handle(int userId)
        {
            var contacts = _contactService.GetContacts(userId);

            if (contacts.Count == 0)
            {
                Console.WriteLine("Your contact list is empty.");
                return;
            }

            Console.WriteLine("Your Contacts:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.Phone}");
            }
        }
    }
}
