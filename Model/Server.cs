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
        public void RunNetwork()
        {
            Socket socket = SeConnecter();
            Socket client = AccepterConnection(socket);
            this.Listen(client);

        }
        private static Socket SeConnecter()
        {
            int port = 11000;
            IPHostEntry ipHostName = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint ipEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(ipEP);
            socket.Listen(port);


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

        private void Listen(Socket client)
        {


        }

        private static void Send(Socket client)
        {
            byte[] bytes = new Byte[1024];
            int i = 0;

            try
            {
                string msg = "name : SAVENAME";
                byte[] bmsg = Encoding.UTF8.GetBytes(msg);
                client.Send(bmsg);
            }
            catch (Exception ex)
            {

            }

            while (true)
            {
                try
                {


                    i = (i + 1) % 100;
                    string msg = "value :" + i.ToString();
                    byte[] bmsg = Encoding.UTF8.GetBytes(msg);
                    client.Send(bmsg);
                    Thread.Sleep(500);

                }
                catch (Exception ex)
                {

                }
            }
        }
        private static void Deconnecter(Socket socket)
        {
            socket.Close();
        }
    }
}

