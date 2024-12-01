using ContactAApp.Handlers;
using ContactAApp.Services;
using ContactAApp.Database;
using ContactAApp.Controllers;

namespace ContactAApp
{
    internal class Application
    {
        private readonly LoginController _loginController;
        private readonly RegisterController _registerController;
        private readonly AddContactHandler _addContactHandler;
        private readonly ViewContactsHandler _viewContactsHandler;
        private readonly UpdateContactHandler _updateContactHandler;
        private readonly DeleteContactHandler _deleteContactHandler;

        public Application()
        {
            string connectionString = Database.Database.GetConnectionString();
            var contactService = new ContactService(connectionString);
            _loginController = new LoginController(connectionString);
            _registerController = new RegisterController(connectionString);

            _viewContactsHandler = new ViewContactsHandler(contactService);
            _addContactHandler = new AddContactHandler(contactService);
            _updateContactHandler = new UpdateContactHandler(contactService, _viewContactsHandler); 
            _deleteContactHandler = new DeleteContactHandler(contactService, _viewContactsHandler); 
        }


        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
                Console.Write("Choose an option: ");
                string mainOption = Console.ReadLine();

                switch (mainOption)
                {
                    case "1":
                        _registerController.Register();
                        break;

                    case "2":
                        var user = _loginController.Login();
                        if (user != null)
                        {
                            Console.WriteLine($"Welcome, {user.Username}!");
                            UserMenu(user.Id);
                        }
                        break;

                    case "3":
                        Console.WriteLine("Exiting program...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private void UserMenu(int userId)
        {
            while (true)
            {
                Console.WriteLine("\n1. Add Contact\n2. View Contacts\n3. Update Contact\n4. Delete Contact\n5. Logout");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _addContactHandler.Handle(userId);
                        break;

                    case "2":
                        _viewContactsHandler.Handle(userId);
                        break;

                    case "3":
                        _updateContactHandler.Handle(userId);
                        break;

                    case "4":
                        _deleteContactHandler.Handle(userId);
                        break;

                    case "5":
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
