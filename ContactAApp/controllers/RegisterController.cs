using System;
using System.Data.SqlClient;
using ContactAdoNet.helpers;

namespace ContactAApp.Controllers
{
    internal class RegisterController
    {
        private readonly string _connectionString;

        public RegisterController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Register()
        {
            string username;
            string password = string.Empty;
            string confirmPassword = string.Empty;

            do
            {
                Console.Write("Enter a Username: ");
                username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Error: Username cannot be empty. Please try again.");
                }

            } while (string.IsNullOrWhiteSpace(username));

            do
            {
                Console.Write("Enter a Password: ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Error: Password cannot be empty. Please try again.");
                    continue;
                }

                Console.Write("Confirm Password: ");
                confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                {
                    Console.WriteLine("Error: Passwords do not match. Please try again.");
                }

            } while (string.IsNullOrWhiteSpace(password) || password != confirmPassword);

            string hashedPassword = PasswordHelper.HashPassword(password);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@username", username);

                    int userCount = (int)checkCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        Console.WriteLine("Error: Username already exists. Please try a different username.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@password", hashedPassword);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Registration successful. You can now log in.");
                    else
                        Console.WriteLine("Error: Registration failed. Please try again.");
                }
            }
        }
    }
}
