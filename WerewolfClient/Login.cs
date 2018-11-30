using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WerewolfClient
{
    public partial class Login : Form, View
    {
        private string server;
        private WerewolfController controller;
        private Form _mainForm;
        public Login(Form MainForm)
        {
            InitializeComponent();
            _mainForm = MainForm;
        }
        
        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case WerewolfModel.EventEnum.SignIn:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            _mainForm.Visible = true;
                            this.Visible = false;

                            TbLogin.Text = "";
                            TbPassword.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case WerewolfModel.EventEnum.SignUp:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            MessageBox.Show("Sign up successfuly, please login", "Success", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
            }
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignIn;

            SetServer(); //set server

            wcmd.Payloads = new Dictionary<string, string>() { { "Login", TbLogin.Text }, { "Password", TbPassword.Text }, { "Server", server } };
            controller.ActionPerformed(wcmd);
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignUp;

            SetServer(); //set server

            wcmd.Payloads = new Dictionary<string, string>() { { "Login", TbLogin.Text}, { "Password",TbPassword.Text}, { "Server", server } };
            controller.ActionPerformed(wcmd);

            TbLogin.Text = "";
            TbPassword.Text = ""; 
        }
        public string GetServer()
        {
            return server;
        }

        public void SetServer()
        {
            if (TBServer.Text == "Server for 2 players")
            {
                server = "http://project-ile.net:2342/werewolf/";
            }
            if (TBServer.Text == "Server for 4 players")
            {
                server = "http://project-ile.net:2344/werewolf/";
            }
            if (TBServer.Text == "Server for 16 players")
            {
                server = "http://project-ile.net:23416/werewolf/";
            }
        }
    }
}
