using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class DB
    {

        //make connection
       private MySqlConnection dbConnection = new MySqlConnection("server=localhost;port=3306;username=root;password='';database=Project_MusicPlayer");

        
        public void OpenConnection()
        {
            if (dbConnection.State == System.Data.ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnection.Close();
            }
        }

        public MySqlConnection GetConn()
        {
            return dbConnection;
        }
    }
}
