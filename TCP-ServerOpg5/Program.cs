using System;

namespace TCP_ServerOpg5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Server server = new Server();
            server.Start();
        }
    }
}
