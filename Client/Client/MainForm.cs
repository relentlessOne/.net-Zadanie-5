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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public static TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;
        Stream fileStream = null;

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text.ToString() + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[1024];
            label1.Text = Convert.ToString((int)clientSocket.ReceiveBufferSize);
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + returndata);
        }

        public void msg(string mesg)
        {
           textBox1.AppendText( textBox1.Text + Environment.NewLine + mesg);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            //  Prompt.ShowDialog(clientSocket);
            new ConnectInputForm().ShowDialog();

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
            fileStream = null;
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
                        using (fileStream)
                        {
                            
                           Console.WriteLine(openFileDialog1.FileName);
                            pathBox.Text = openFileDialog1.FileName;


                        }
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
