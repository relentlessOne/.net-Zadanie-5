using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClinet client = new handleClinet();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }
    }

    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
     
            byte[] buf ;
            byte[] bufFileInfo = new byte[1024];
            string fileInfo;
            string fileInfoCollected;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string fileName = "";
            int fileSize = 0;



            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                
                 
                        networkStream.Read(bufFileInfo, 0, bufFileInfo.Length);
                        fileInfoCollected = System.Text.Encoding.ASCII.GetString(bufFileInfo);
                        Console.WriteLine(fileInfoCollected.Substring(0, fileInfoCollected.IndexOf('$')));
                        fileInfo = fileInfoCollected.Substring(0, fileInfoCollected.IndexOf('$'));
                    
                         fileSize = Convert.ToInt32(fileInfo.Substring(0, fileInfo.IndexOf('|')));
                         fileName = fileInfo.Substring(fileInfo.IndexOf('|') + 1);

                        Console.WriteLine(fileName);
                       
           
         
                        buf = new byte[fileSize];
                        networkStream.Read(buf, 0, buf.Length);
                        
                        string pathDebug = Directory.GetCurrentDirectory();
                        string path = pathDebug.Substring(0, pathDebug.Length - 9);
                        path += "FilesFromClient\\";

                        bool exists = System.IO.Directory.Exists(path);

                        if (!exists)
                            System.IO.Directory.CreateDirectory(path);

                        int count = 0;
     
                        string beforeDot = fileName.Substring(0, fileName.IndexOf('.'));
                        string afterDot = fileName.Substring(fileName.IndexOf('.'));

                 


                        bool fileExist = File.Exists(@path + fileName);
                        while (fileExist)
                        {

                            count++;
                            fileExist = File.Exists(@path + beforeDot + "-" + count + afterDot);
                        }

                        if (count == 0)
                        {
                            File.WriteAllBytes(@path + fileName, buf);
                         //   serverResponse = "DONE";
                         //   sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                         //   networkStream.Write(sendBytes, 0, sendBytes.Length);
                         //networkStream.Flush();

                    }
                         else
                        {
                            File.WriteAllBytes(@path + beforeDot + "-" + count + afterDot, buf);
                            serverResponse = "DONE";
                            //sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            //networkStream.Write(sendBytes, 0, sendBytes.Length);
                            // networkStream.Flush();  


                        }

                 



          
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
