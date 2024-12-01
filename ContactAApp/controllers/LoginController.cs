using System;
using System.Data.SqlClient;
using ContactAdoNet.helpers;
using ContactAdoNet.models;

namespace ContactAApp.Controllers
{
    internal class LoginController
    {
        private readonly string _connectionString;

        public LoginController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User Login()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            string hashedPassword = PasswordHelper.HashPassword(password);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPassword);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                            return null;
                        }
                    }
                }
            }
        }
    }
}
