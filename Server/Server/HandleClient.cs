using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class HandleClient
    {
        static TcpClient clientSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(fileHandler);
            ctThread.Start();
        }
        private void fileHandler()
        {

            byte[] buf;
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
                        serverResponse = "DONE";
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();

                    }
                    else
                    {
                        File.WriteAllBytes(@path + beforeDot + "-" + count + afterDot, buf);
                        serverResponse = "DONE";
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Client has been disconnected");
                    break;
                }
            }
        }
    
}
}
