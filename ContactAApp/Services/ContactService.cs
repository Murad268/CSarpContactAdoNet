using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ContactAdoNet.models;

namespace ContactAApp.Services
{
    internal class ContactService
    {
        private readonly string _connectionString;

        public ContactService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddContactToDatabase(int userId, string name, string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Contacts (UserId, Name, Phone) VALUES (@userId, @name, @phone)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Contact> GetContacts(int userId)
        {
            var contacts = new List<Contact>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts WHERE UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Phone = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return contacts;
        }

        public Contact GetContactById(int contactId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts WHERE Id = @contactId AND UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Contact
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Phone = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateContactInDatabase(int contactId, int userId, string name, string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Contacts SET Name = @name, Phone = @phone WHERE Id = @contactId AND UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteContactFromDatabase(int contactId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Contacts WHERE Id = @contactId AND UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
