using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class RegistrationWindow : Form
    {
        public RegistrationWindow(Form f)
        {
            host = f;
            InitializeComponent();
        }

        private void ConnectBut_Click(object sender, EventArgs e)
        {
            login = LoginBox.Text;
            ip = IPBox.Text;
            port = PortBox.Text;
            host.Enabled = true;
            host.Visible = true;
            this.Visible=false;
        }
        public String Login
        {
            get { return login; }
        }
        public String IP
        {
            get { return ip; }
        }
        public String Port
        {
            get { return port; }
        }
        private Form host;
        private String login;
        private String ip;
        private String port;

        private void RegistrationWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
        }
    }
}
