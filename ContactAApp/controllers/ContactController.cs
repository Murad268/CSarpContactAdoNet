using System;
using System.Data.SqlClient;
using ContactAApp.helpers;
using ContactAdoNet.models;
using ContactAApp.helpers;
namespace ContactAApp.Controllers
{
    internal class ContactController
    {
        private readonly string _connectionString;

        public ContactController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddContact(int userId)
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
                    Console.WriteLine("Error: Invalid phone number. It must contain only digits, '+', '(', ')' and be at least 7 characters long. Please try again.");
                }

            } while (!PhoneHelper.ValidatePhoneNumber(phone)); // Telefon düzgün olmadığı halda dövrə davam edir

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Contacts (UserId, Name, Phone) VALUES (@userId, @name, @phone)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.ExecuteNonQuery();

                    Console.WriteLine("Contact added successfully.");
                }
            }
        }

        public bool ViewContacts(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts WHERE UserId = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) 
                        {
                            Console.WriteLine("Your contact list is empty.");
                            return false;
                        }

                        Console.WriteLine("Your Contacts:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader.GetInt32(0)}, Name: {reader.GetString(2)}, Phone: {reader.GetString(3)}");
                        }
                        return true;
                    }
                }
            }
        }




        public void UpdateContact(int userId)
        {
            if (!ViewContacts(userId))
            {
                Console.WriteLine("Update operation is not available because your contact list is empty.");
                return;
            }

            Console.Write("Enter Contact ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                Console.WriteLine("Invalid Contact ID.");
                return;
            }

            string oldName = string.Empty;
            string oldPhone = string.Empty;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Name, Phone FROM Contacts WHERE Id = @contactId AND UserId = @userId";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@contactId", contactId);
                    selectCommand.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Console.WriteLine("Contact not found or you do not have permission.");
                            return;
                        }

                        oldName = reader.GetString(0);
                        oldPhone = reader.GetString(1);
                    }
                }

                Console.WriteLine($"Current Name: {oldName}");
                Console.WriteLine($"Current Phone: {oldPhone}");

                Console.Write("Enter New Name (leave blank to keep current): ");
                string newName = Console.ReadLine();

                string newPhone;

                do
                {
                    Console.Write("Enter New Phone (leave blank to keep current): ");
                    newPhone = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPhone))
                    {
                        newPhone = oldPhone;
                        break;
                    }

                    if (!PhoneHelper.ValidatePhoneNumber(newPhone))
                    {
                        Console.WriteLine("Error: Invalid phone number. Please try again.");
                    }

                } while (!PhoneHelper.ValidatePhoneNumber(newPhone));

                newName = string.IsNullOrWhiteSpace(newName) ? oldName : newName;

                string updateQuery = "UPDATE Contacts SET Name = @newName, Phone = @newPhone WHERE Id = @contactId AND UserId = @userId";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newName", newName);
                    updateCommand.Parameters.AddWithValue("@newPhone", newPhone);
                    updateCommand.Parameters.AddWithValue("@contactId", contactId);
                    updateCommand.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Contact updated successfully.");
                    else
                        Console.WriteLine("Failed to update the contact.");
                }
            }
        }




        public void DeleteContact(int userId)
        {
            if (!ViewContacts(userId))
            {
                Console.WriteLine("Delete operation is not available because your contact list is empty.");
                return;
            }

            Console.Write("Enter Contact ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                Console.WriteLine("Invalid Contact ID.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Contacts WHERE Id = @contactId AND UserId = @userId";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@contactId", contactId);
                    deleteCommand.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Contact deleted successfully.");
                    else
                        Console.WriteLine("Contact not found or you do not have permission.");
                }
            }
        }



    }
}
