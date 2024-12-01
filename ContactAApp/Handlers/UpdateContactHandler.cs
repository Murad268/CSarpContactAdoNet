using System;
using ContactAApp.Services;
using ContactAApp.helpers;

namespace ContactAApp.Handlers
{
    internal class UpdateContactHandler
    {
        private readonly ContactService _contactService;

        public UpdateContactHandler(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void Handle(int userId)
        {
            Console.Write("Enter Contact ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                Console.WriteLine("Invalid Contact ID.");
                return;
            }

            var contact = _contactService.GetContactById(contactId, userId);
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine($"Current Name: {contact.Name}");
            Console.WriteLine($"Current Phone: {contact.Phone}");

            Console.Write("Enter New Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            newName = string.IsNullOrWhiteSpace(newName) ? contact.Name : newName;

            Console.Write("Enter New Phone (leave blank to keep current): ");
            string newPhone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newPhone))
            {
                newPhone = contact.Phone;
            }
            else if (!PhoneHelper.ValidatePhoneNumber(newPhone))
            {
                Console.WriteLine("Invalid phone number.");
                return;
            }

            _contactService.UpdateContactInDatabase(contactId, userId, newName, newPhone);
            Console.WriteLine("Contact updated successfully.");
        }
    }
}
