using System;
using ContactAApp.Services;

namespace ContactAApp.Handlers
{
    internal class DeleteContactHandler
    {
        private readonly ContactService _contactService;
        private readonly ViewContactsHandler _viewContactsHandler;

        public DeleteContactHandler(ContactService contactService, ViewContactsHandler viewContactsHandler)
        {
            _contactService = contactService;
            _viewContactsHandler = viewContactsHandler;
        }

        public void Handle(int userId)
        {
            if (!_viewContactsHandler.Handle(userId))
            {
                return;
            }

            Console.Write("Enter Contact ID to delete: ");
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

            _contactService.DeleteContactFromDatabase(contactId, userId);
            Console.WriteLine("Contact deleted successfully.");
        }
    }
}
