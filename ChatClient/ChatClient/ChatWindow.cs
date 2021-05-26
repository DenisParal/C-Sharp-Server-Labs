using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class ChatClient : Form
    {
        class Receiver
        {
            public Receiver(ChatClient form)
            {
                thread = new Thread(Work);
                thread.Start(form);
            }
            private void Work(object obj)
            {
                ChatClient form = (ChatClient)obj;
                while (form.client.Connected)
                {
                    form.ChatBox.Invoke((MethodInvoker)delegate
                    {
                        form.ChatBox.Text += form.client.RecieveMessage();
                    });
                }
            }
            public void Stop()
            {
                thread.Join();
            }
            private Thread thread;
        }
        public ChatClient()
        {
            InitializeComponent();
        }

        private void SendBut_Click(object sender, EventArgs e)
        {
            client.SendMessage(MessageBox.Text);
            Thread.Sleep(1);
            MessageBox.Clear();
        }

        private void ChatClient_Load(object sender, EventArgs e)
        {
            reg = new RegistrationWindow(this);
            reg.Visible = true;
            this.Visible = false;
            this.Enabled = false;
        }

        Receiver rec;
        RegistrationWindow reg;
        Client client;

        private void LeaveBut_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            reg.Visible = true;
            this.Enabled = false;
        }

        private void ChatClient_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                client = new Client(reg.Login);
                client.Connect(reg.IP, Int32.Parse(reg.Port));
                rec = new Receiver(this);
            }
        }

        private void ChatClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(client!=null)
            {
                client.Disconnect();
                rec.Stop();
            }
        }
    }
}
