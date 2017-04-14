using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ConnectInputForm : Form
    {
  
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }



        public ConnectInputForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            ipBox.Text = "127.0.0.1";
            portBox.Text = "8888";
            loadGif.Hide();

        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;
            string msg = "Wrong: ";
            bool failed = false;

            if(!IPAddress.TryParse(ipBox.Text,out ip) )
            {
                msg += "Ip Adress; ";
                failed = true;
            }

            if(!Int32.TryParse(portBox.Text, out port))
            {
                msg += "Port;";
                failed = true;
            }

            if (failed)
            {
                MessageBox.Show(msg);
            } else
            {

                loadAnimStart();
                new Thread(() =>
                {
                    try
                    {
     
                        MainForm.clientSocket.Connect(new IPEndPoint(ip, port));
                        JavaScript.SetTimeout(() =>
                        {
                            Invoke(new Action(() =>
                            {
                                this.Close();
                            }));
                        }, 2000);


                    } catch(Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            loadAnimStop();
                            MessageBox.Show("Could not connect to the server");
                        }));

                    }
                }).Start();
            }
          

        }

        private void loadAnimStop()
        {
            label1.Text = "IP Adress";
            loadGif.Hide();
            label2.Show();
            ipBox.Show();
            portBox.Show();
            exitBtn.Show();
            connectBtn.Show();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadAnimStart()
        {
            label2.Hide();
            ipBox.Hide();
            portBox.Hide();
            exitBtn.Hide();
            connectBtn.Hide();
            label1.Text = "Connecting ...";
            loadGif.Show();
        }


    }
}
