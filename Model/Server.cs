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
        public void RunNetwork()
        {
            Socket socket = SeConnecter();
            Socket client = AccepterConnection(socket);
            //this.ListenTo(client);
            Thread.Sleep(2000);
            this.SendToTo(client, "name");

        }
        private static Socket SeConnecter()
        {
            int port = 11000;
            IPHostEntry ipHostName = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint ipEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
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
            catch (Exception ex)
            {

            }
            return null;
        }

        private void ListenTo(Socket client)
        {
            byte[] bytes = new Byte[1024];
            List<Backup> list = MainWindow.GetMainWindow().BackupList;

            while (true)
            {
                try
                {
               //     Dispatcher BackupList[index].Name; });
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void SendTo(Socket client, string name)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                buffer = Encoding.UTF8.GetBytes(name.ToString());
                client.Send(buffer);
            }
        }
        public void SendToTo(Socket client, string name)
        {
                //if (client != null)
                //{
                //string msg = "action : play";
                byte[] bmsg = new byte[1024];
                bmsg = Encoding.ASCII.GetBytes(name);
                    try
                    {
                while (true)
                {
                    client.Send(bmsg);
                    //Thread.Sleep(10000);
                }
                    }
                    catch (Exception ex)
                    {

                    }
            
        }
        private static void Deconnecter(Socket socket)
        {
            socket.Close();
        }
    }
}

