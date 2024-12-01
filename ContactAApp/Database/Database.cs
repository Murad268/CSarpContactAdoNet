namespace ContactAApp.Database
{
    internal class Database
    {
        private static string _connectionString = "Server=DESKTOP-U7NODTP;Database=contactProject;User Id=MuradContact;Password=1;Integrated Security=SSPI;";

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
