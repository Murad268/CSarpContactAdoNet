using ContactAApp.Controllers;
using ContactAApp.Database;
namespace ContactAApp
{
    internal class Application
    {
        private readonly LoginController _loginController;
        private readonly RegisterController _registerController;
        private readonly ContactController _contactController;

        public Application()
        {
            string connectionString = Database.Database.GetConnectionString();
            _loginController = new LoginController(connectionString);
            _registerController = new RegisterController(connectionString);
            _contactController = new ContactController(connectionString);
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
                        _contactController.AddContact(userId);
                        break;
                    case "2":
                        _contactController.ViewContacts(userId);
                        break;
                    case "3":
                        _contactController.UpdateContact(userId);
                        break;
                    case "4":
                        _contactController.DeleteContact(userId);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
