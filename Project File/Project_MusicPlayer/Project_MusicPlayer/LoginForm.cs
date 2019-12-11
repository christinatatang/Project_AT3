using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_MusicPlayer
{
    /*
         Name: Christina Tatang
         ID: 30003663
         DoB: 02/08/2000
        Project - Programming III 
        This is a project about music player application. 
        */
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //to log in user
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            
            DB database = new DB();
            HashTech hash = new HashTech();

            //get the input string
            string username = tbUser.Text;
            string password = tbPassword.Text;

            database.OpenConnection();

            //get the password and username
            MySqlCommand command = new MySqlCommand("SELECT `password`,`salt` FROM `usertable` WHERE `username` = @user", database.GetConn());

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;

            MySqlDataReader login = command.ExecuteReader();
            if (login.Read())
            {
                string resultPassword = login.GetString("password");
                string resultSalt = login.GetString("salt");

                bool isMatch = hash.IsPasswordMatch(password, resultSalt, resultPassword);
                //if the hash password is match
                if (isMatch)
                {
                    string user = tbUser.Text;
                    this.Hide();
                    MusicPlayerForm MusicPlayerForm = new MusicPlayerForm(user);
                    MusicPlayerForm.Show();
                }
                else
                {
                    lblMessage.Text = "Incorrect";
                }

            }

        }

        //go to  register form
        private void LLblCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterForm regisForm = new RegisterForm();
            regisForm.Show();
        }

        //close form
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
