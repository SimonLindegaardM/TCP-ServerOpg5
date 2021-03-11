using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_ServerOpg5
{
    public class Server
    {
        private static List<Beer> _beers = new List<Beer>()
        {
            new Beer {Id = 1, Name = "Corona", Price = 10, Abv = 20},
            new Beer {Id = 2, Name = "Tuborg", Price = 20, Abv = 10},
            new Beer {Id = 3, Name = "Carlsberg", Price = 5, Abv = 25},
            new Beer {Id = 4, Name = "Pilsner", Price = 15, Abv = 15}
        };


        public void Start()
        {
            
                TcpListener server = new TcpListener(IPAddress.Loopback, 4646);

                server.Start();
                Console.WriteLine("Server has started");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client has entered the server");
                    Task.Run(() =>
                    {
                        TcpClient tempSocket = client;
                        DoClient(tempSocket);
                    });
                }
        }

        private void DoClient(TcpClient tempSocket)
        {
            Stream ns = tempSocket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            string message = sr.ReadLine();

            while (message != " " && message != "stop")
            {
                switch (message)
                {
                    case "GetAll":
                        sw.WriteLine(JsonConvert.SerializeObject(_beers));
                        break;
                    case "Get": //First write Get, and then on the following line, write the Id of the beer you wanna find.
                        int id = Convert.ToInt32(sr.ReadLine());
                        sw.WriteLine(_beers.Find(beer => beer.Id == id));
                        break;
                    case "Save": //First write Save, and then on the following line type the beer you wanna save as a jsonString.
                        string json = sr.ReadLine();
                        Beer savedBeer = JsonConvert.DeserializeObject<Beer>(json);
                        _beers.Add(savedBeer);
                        break;
                }
                message = sr.ReadLine();
            }
            ns.Close();
            tempSocket.Close();
        }
    }
}
