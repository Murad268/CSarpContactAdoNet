using ContactAApp.Controllers;
using static System.Net.Mime.MediaTypeNames;

namespace ContactAApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run();
        }
    }
}
