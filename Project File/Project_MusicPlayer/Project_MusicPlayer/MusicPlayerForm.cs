using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

    //The project must have GUI
    public partial class MusicPlayerForm : Form
    {
        User myUser = new User();
        
        
        public MusicPlayerForm(string user)
        {
            InitializeComponent();
            //get user name from other form and store it in class
            lblUsername.Text += user + "!";
            myUser.MyUser = user;

        }

        //Must contain dynamic data structure
        LinkedList<string> myAudioPlayer = new LinkedList<string>();
        string[] paths;

        //display song to list box
        private void DisplayList()
        {
            LbOutput.Items.Clear();
            foreach (string song in myAudioPlayer)
            {
                LbOutput.Items.Add(System.IO.Path.GetFileName(song));
            }
        }

        //To play the current song or if doesnt have current, it will play the first song
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            LinkedListNode<string> current = myAudioPlayer.Find(WMPlayer.URL);
            if (current == null)
            {
                WMPlayer.URL = myAudioPlayer.First.Value;
            }
            else if (current != null){
                WMPlayer.URL = current.Value;
            }
            else
            {
                MessageBox.Show("Error");
            }
            
        }

        //To play the previous song from current
        private void BtnPrev_Click(object sender, EventArgs e)
        {
            LinkedListNode<string> current = myAudioPlayer.Find(WMPlayer.URL);
            if (current != myAudioPlayer.First)
            {
                WMPlayer.URL = current.Previous.Value;
            }
            else
            {
                MessageBox.Show("Error! This is the begining of playlist.");
            }
        }

        //To play the first song 
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                WMPlayer.URL = myAudioPlayer.First.Value;
            }
            catch
            {
                MessageBox.Show("Error. The first song is being played.");
            }
        }

        //To play the next song from current
        private void BtnNext_Click(object sender, EventArgs e)
        {
            var current = myAudioPlayer.Find(WMPlayer.URL);
            if (current != myAudioPlayer.Last)
            {
                WMPlayer.URL = current.Next.Value;
            }
            else
            {
                MessageBox.Show("Error! This is the end of playlist.");
            }
        }

        //To play the last song from playlist
        private void BtnLast_Click(object sender, EventArgs e)
        {
            try
            {
                WMPlayer.URL = myAudioPlayer.Last.Value;
            }
            catch
            {
                MessageBox.Show("Error. The last song is being played.");
            }
        }


        //Must contain searching techniques
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            bool found = false;
            int min = 0;
            int max = myAudioPlayer.Count - 1;
            string target = TbSearch.Text + ".mp3";

            if (string.IsNullOrWhiteSpace(TbSearch.Text))
            {
                MessageBox.Show("Please enter a search work");
            }
            else
            {
                //using binary search
                while (min <= max)
                {
                    int mid = ((min + max) / 2);
                    if (target.CompareTo(Path.GetFileName(myAudioPlayer.ElementAt(mid))) == 0)
                    {
                        found = true;
                        DisplayList();
                        LbOutput.SetSelected(mid, true);
                        break;
                    }
                    else if (target.CompareTo(Path.GetFileName(myAudioPlayer.ElementAt(mid))) < 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }
                //if not found, display the song contains the words
                if (!found)
                {
                    max = myAudioPlayer.Count - 1;
                    target = TbSearch.Text;
                    LbOutput.Items.Clear();
                    for (min = 0; min <= max; min++)
                    {
                        if (myAudioPlayer.ElementAt(min).ToLower().Contains(target.ToLower()))
                        {
                            LbOutput.Items.Add(System.IO.Path.GetFileName(myAudioPlayer.ElementAt(min)));
                        }
                    }
                }
            }
        }

        //Must contain sorting algorithm 
        void BubbleSort(LinkedList<string> list)
        {
            for (int i = 0; i < myAudioPlayer.Count; i++)
            {
                for (int j = 0; j < myAudioPlayer.Count - 1; j++)
                {
                    if (string.CompareOrdinal(list.ElementAt(j), list.ElementAt(j + 1)) >= 0)
                    {
                        LinkedListNode<string> current = list.Find(list.ElementAt(j));
                        var temp = current.Next.Value;
                        current.Next.Value = current.Value;
                        current.Value = temp;
                        
                    }
                }
            }
        }

        //save the playlist into the database
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LbOutput.Items.Count != 0)
            {
                string mySong = null;
                foreach (string song in myAudioPlayer)
                {
                    mySong += song.ToString() + ";";
                }

                DB database = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("UPDATE `usertable` SET `song`= @song WHERE `username` = @user", database.GetConn());

                command.Parameters.Add("@user", MySqlDbType.VarChar).Value = myUser.MyUser;
                command.Parameters.Add("@song", MySqlDbType.LongText).Value = mySong;

                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            else
            {
                MessageBox.Show("No song found");
            }
        }

        //get the playlist from the database
        private void MusicPlayerForm_Load(object sender, EventArgs e)
        {
            DB database = new DB();

            database.OpenConnection();
            MySqlCommand command = new MySqlCommand("SELECT `song` FROM `usertable` WHERE `username` = @user", database.GetConn());

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = myUser.MyUser;

            MySqlDataReader login = command.ExecuteReader();
            if (login.Read())
            {
                string mySong = login.GetString("song");
                string[] song = mySong.Split(';');
                foreach (string theSong in song)
                {
                    if (System.IO.File.Exists(theSong))
                    {
                        myAudioPlayer.AddLast(theSong);
                    }
                }
                DisplayList();
                database.CloseConnection();
            }
        }

        //to clear the text box and display the playlist
        private void TbSearch_DoubleClick(object sender, EventArgs e)
        {
            TbSearch.Clear();
            LbOutput.Items.Clear();
            DisplayList();
        }

        //when the user double click the song in the list box
        private void LbOutput_DoubleClick(object sender, EventArgs e)
        {
            string current = LbOutput.SelectedItem.ToString();

            int max = myAudioPlayer.Count - 1;
            for(int min =0; min<=max; min++)
            {
                if (myAudioPlayer.ElementAt(min).Contains(current))
                {
                    var curr = myAudioPlayer.Find(myAudioPlayer.ElementAt(min));
                    WMPlayer.URL = curr.Value;
                    break;
                }
            }
        }

        //to delete the song from playlist
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string current = LbOutput.SelectedItem.ToString();

            int max = myAudioPlayer.Count - 1;
            for(int min =0; min<=max; min++)
            {
                if (myAudioPlayer.ElementAt(min).Contains(current))
                {
                    var curr = myAudioPlayer.Find(myAudioPlayer.ElementAt(min));
                    myAudioPlayer.Remove(curr);
                    DisplayList();
                    break;
                }
            }
        }

        //to log out from current user
        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }

        //when the user close the form
        private void MusicPlayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //To open or add new song
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBinary = new OpenFileDialog();
            OpenBinary.Multiselect = Enabled;
            DialogResult dr = OpenBinary.ShowDialog();
            if (dr == DialogResult.OK)
            {
                paths = OpenBinary.FileNames;
                for (int i = 0; i < paths.Length; i++)
                {
                    myAudioPlayer.AddLast(paths[i]);
                    BubbleSort(myAudioPlayer);
                    DisplayList();
                }
            }
            else
            {
                MessageBox.Show("Error. Cannot open file.");
            }
        }

        //about this project
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a music player application." +
                "\nThis is a project for programming III." +
                "\nThe user should be able to log in or create user to play the music." +
                "\nClick help button if you want to know more about this project. ");
        }

        //will open a help file about this project.
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "Help.html";

            Process.Start(filename);
        }
    }
}
