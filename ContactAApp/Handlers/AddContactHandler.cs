using System;
using ContactAApp.Services;
using ContactAApp.helpers;

namespace ContactAApp.Handlers
{
    internal class AddContactHandler
    {
        private readonly ContactService _contactService;

        public AddContactHandler(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void Handle(int userId)
        {
            string name;
            string phone;

            do
            {
                Console.Write("Enter Contact Name: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Error: Contact name cannot be empty. Please try again.");
                }

            } while (string.IsNullOrWhiteSpace(name));

            do
            {
                Console.Write("Enter Contact Phone: ");
                phone = Console.ReadLine();

                if (!PhoneHelper.ValidatePhoneNumber(phone))
                {
                    Console.WriteLine("Error: Invalid phone number. Please try again.");
                }

            } while (!PhoneHelper.ValidatePhoneNumber(phone));

            _contactService.AddContactToDatabase(userId, name, phone);

            Console.WriteLine("Contact added successfully.");
        }
    }
}
