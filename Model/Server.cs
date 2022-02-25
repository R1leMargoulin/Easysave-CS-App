using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace EasySave.Model
{
    public class Server
    {
        public Server()
        {

        }
        /*public void RunNetwork(string name)
        {
            Socket socket = SeConnecter();
            Socket client = AccepterConnection(socket);
            //this.ListenTo(client);
            Thread.Sleep(2000);
            this.SendToTo(client,name);

        }*/
        public void RunNetwork() //Launh the connection and send to the client a string
        {
            Socket socket = SeConnecter();
            Socket client = AccepterConnection(socket);
            //this.ListenTo(client);
            Thread.Sleep(2000);
            this.SendToTo(client, "name");

        }
        private static Socket SeConnecter()
        {
            int port = 11000;                                                   // Initialise the port need to be the same port on client
            IPHostEntry ipHostName = Dns.GetHostEntry(Dns.GetHostName());       // Get the dns name of the server
            IPAddress ipAddress = IPAddress.Any;                                // Set at an IPAddress all the IP of the server
            IPEndPoint ipEP = new IPEndPoint(ipAddress, port);                  // Set the endpoint with the IP address and the port

            // Create a TCP/IP socket.
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try          //try to launch the connection and listen on the port
            {
                socket.Bind(ipEP);
                socket.Listen(port);


            }
            catch (Exception e)
            {
                MessageBox.Show("Vérifier votre connexion réseau");
            }
        
        return socket;
        }

        private static Socket AccepterConnection(Socket socket)
        {
            try
            {
                while (true)
                {
                    // Program is suspended while waiting for an incoming connection.  
                    Socket distant = socket.Accept();
                    return distant;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        private void ListenTo(Socket client)
        {
            byte[] bytes = new Byte[1024];
            List<Backup> list = MainWindow.GetMainWindow().BackupList;


        }

        /*private void SendTo(Socket client, string name)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                buffer = Encoding.UTF8.GetBytes(name.ToString());
                client.Send(buffer);
            }
        }*/
        public void SendToTo(Socket client, string name)   //sent a string to the client
        {

                byte[] bmsg = new byte[1024];
                bmsg = Encoding.ASCII.GetBytes(name);
        try
        {
            while (true)
            {
                client.Send(bmsg);
                Thread.Sleep(500);

            }
        }
        catch (Exception)
        {
        }
        }

            
        
        private static void Deconnecter(Socket socket)  //Permit to close the socket connection
        {
            socket.Close();
        }
    }
}


