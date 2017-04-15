using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public static TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;
        Stream fileStream = null;
        byte[] fileBuffer;
        byte[] sizeAndName;

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           



            new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    sendBtn.Enabled = false;
                    loadFile.Enabled = false;
                }));
                serverStream.Write(sizeAndName, 0, sizeAndName.Length);
                serverStream.Flush();
                Thread.Sleep(500);
                fileStream.Read(fileBuffer, 0, fileBuffer.Length);
                serverStream.Write(fileBuffer, 0, fileBuffer.Length);
                serverStream.Flush();
                JavaScript.SetTimeout(() =>
                {
                    Invoke(new Action(() =>
                    {
                        sendBtn.Enabled = true;
                        loadFile.Enabled = true;
                    }));
                }, 200);

            }).Start();

     
            //try
            //{

            //byte[] inStream = new byte[1024];
            //label1.Text = Convert.ToString((int)clientSocket.ReceiveBufferSize);
            //serverStream.Read(inStream, 0, inStream.Length);
            //string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            //msg("Data from Server : " + returndata);
            //}
            //catch (Exception ex)
            //{
            //MessageBox.Show("Connection to the server has been lost");
            //Application.Restart();
            //Application.ExitThread();
            //}
        }

        public void msg(string mesg)
        {
           textBox1.AppendText( textBox1.Text + Environment.NewLine + mesg);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            //  Prompt.ShowDialog(clientSocket);
            new ConnectInputForm().ShowDialog();
            serverStream = clientSocket.GetStream();

            // try
            // {
            //clientSocket.Connect(new IPEndPoint(IPAddress.Parse("147.0.0.1"), 8888));
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            //// clientSocket.Connect("127.0.0.1", 8888);
            // msg("Client connected to the server");
        }

        private void loadFile_Click(object sender, EventArgs e)
        {
            if(fileStream != null)
                fileStream.Dispose();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((fileStream = openFileDialog1.OpenFile()) != null)
                    {
                       
                            pathBox.Text = openFileDialog1.FileName;     
                            fileBuffer = new byte[fileStream.Length];
                            string size = Convert.ToString(fileStream.Length);
                            string fileName = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1);
                            sizeAndName = System.Text.Encoding.ASCII.GetBytes(size + "|" + fileName + "$");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


    }
}
