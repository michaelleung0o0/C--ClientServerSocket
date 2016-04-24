using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace asg2_server_15208249
{
    class Program
    {
        private static int[] weight = { 8, 7, 6, 5, 4, 3, 2, 1 };

        public static bool check_HKBU(int id)
        {
            int remainder = 0;
            char[] temp = id.ToString().ToCharArray();
            for (int i = 0; i <= 7; i++){
                remainder = remainder + (weight[i] * (int)Char.GetNumericValue(temp[i]));
            }
            if (remainder % 11 == 0){
                return true;
            }
            else{
                return false;
            }
        }

        public static void Main()
        {
            int data;
            int portNum = 4321;

            IPAddress myIP;
            TcpListener Serverbind;

            Socket serverSocket;
            NetworkStream inOutStream;
            BinaryReader readData;
            BinaryWriter writeData;

            myIP = IPAddress.Parse("127.0.0.1");

            Serverbind = new TcpListener(myIP, portNum);
            Serverbind.Start();
            IPEndPoint clientSide = new IPEndPoint(0, 0);

            while(true)
            {
                System.Console.WriteLine("Listening to TCP port# " + portNum);

                serverSocket = Serverbind.AcceptSocket();

                System.Console.WriteLine("\nEstablished connection to client " +
                "IP:" + ((IPEndPoint)serverSocket.RemoteEndPoint).Address.ToString() +
                " Port:" + ((IPEndPoint)serverSocket.RemoteEndPoint).Port.ToString());

                inOutStream = new NetworkStream(serverSocket);
                readData = new BinaryReader(inOutStream);
                writeData = new BinaryWriter(inOutStream);

                data = readData.ReadInt32();
                System.Console.WriteLine("The input ID: " + data + "\n");
                writeData.Write(check_HKBU(data));
                inOutStream.Close();
            }
        }
    }
}
