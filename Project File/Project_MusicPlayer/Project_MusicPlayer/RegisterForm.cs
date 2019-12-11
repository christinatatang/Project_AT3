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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        //to create new user
        private void BtnCreate_Click(object sender, EventArgs e)
        {

            DB database = new DB();
            HashTech hash = new HashTech();

            string username = tbUser.Text;
            string salt = null;
            string password = hash.GeneratePasswordHash(tbPassword.Text, out salt);

            //if the password and confirm password is match
            if (tbPassword.Text != tbConfirmPassword.Text)
            {
                lblMessage.Text = "Password not match";
            }
            else
            {
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `usertable` WHERE `username` = @user", database.GetConn());

                command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                //if there is no same username, create new user
                if (table.Rows.Count == 0)
                {
                    DataTable table2 = new DataTable();
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter();

                    MySqlCommand command2 = new MySqlCommand("INSERT INTO `usertable`(`Username`, `Password`,`salt`) VALUES (@user2,@password,@salt)", database.GetConn());

                    command2.Parameters.Add("@user2", MySqlDbType.VarChar).Value = username;
                    command2.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                    command2.Parameters.Add("@salt", MySqlDbType.VarChar).Value = salt;

                    adapter2.SelectCommand = command2;
                    adapter2.Fill(table2);

                    MessageBox.Show("Sucessfully create new user.");

                    this.Hide();
                    MusicPlayerForm MusicPlayerForm = new MusicPlayerForm(tbUser.Text);
                    MusicPlayerForm.Show();
                }
                else
                {
                    lblMessage.Text = "The user already exist";
                }

            }
        }

        //go to login form
        private void LLblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        //close form
        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
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
