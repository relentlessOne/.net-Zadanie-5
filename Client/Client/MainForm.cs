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
        string fileName;
        string size;

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            waitGif.Hide();

            textBox1.AppendText("Choose file to send");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            new Thread(() =>
            {

                try
                {
                    Invoke(new Action(() =>
                    {
                        sendBtn.Enabled = false;
                        loadFile.Enabled = false;
                        waitGif.Show();

                        textBox1.AppendText(Environment.NewLine + "Sending file: \"" + fileName + "\" size: " + size + " bytes");
                    }));
                    serverStream.Write(sizeAndName, 0, sizeAndName.Length);
                    serverStream.Flush();
                    Thread.Sleep(500);
                    fileStream.Read(fileBuffer, 0, fileBuffer.Length);
                    serverStream.Write(fileBuffer, 0, fileBuffer.Length);
                    serverStream.Flush();
                    Thread.Sleep(500);

                    Invoke(new Action(() =>
                    {

                        byte[] inStream = new byte[1024];

                        if (serverStream.Read(inStream, 0, inStream.Length) == 4)
                        {
                            textBox1.AppendText(Environment.NewLine + "File sended successfully");
                            sendBtn.Enabled = true;
                            loadFile.Enabled = true;
                            waitGif.Hide();
                        }

                    }));
                }
                catch(Exception ez)
                {
                    MessageBox.Show("Connection to the server has been lost");
                    Application.Restart();
                    Application.ExitThread();
                }



            }).Start();

        }

        public void msg(string mesg)
        {
           textBox1.AppendText( textBox1.Text + Environment.NewLine + mesg);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            new ConnectInputForm().ShowDialog();
            serverStream = clientSocket.GetStream();
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
                            size = Convert.ToString(fileStream.Length);
                            fileName = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1);
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
