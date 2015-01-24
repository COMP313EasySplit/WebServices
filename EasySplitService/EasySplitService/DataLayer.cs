using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EasySplitService
{
    public class DataLayer
    {
        //Initializing variable for database connection
        static string connetionString = "Data Source=54.191.15.241;Initial Catalog=EasySplit;Persist Security Info=True;User ID=esusercomp313;Password=comp313esuser";
        SqlConnection con = new SqlConnection(connetionString);

        public bool login(string id, string password)
        {
            bool found = false;

            SqlCommand command = new SqlCommand("Select * from TUser where Email='" + id.Trim()+"'", con);
            SqlDataReader dataReader;
            con.Open();
            dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    if (password.Trim() == dataReader.GetValue(4).ToString())
                    {
                        found = true;
                        con.Close();
                        return found;
                    }
                }
            }
            else
            {
                con.Close();
                return found;
            }

            con.Close();
            return found;
        }

        public int registerNewUser(string firstName, string lastName, string email, string password)
        {
            int registered = 0;

            SqlCommand cmd = new SqlCommand("INSERT INTO TUser (Firstname, Lastname, Email, Password) VALUES (@Firstname, @Lastname, @Email, @Password)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Firstname", firstName);
            cmd.Parameters.AddWithValue("@Lastname", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            registered=cmd.ExecuteNonQuery();
            con.Close();

            return registered;
        }

        public int addNewCategory(string name, int userid)
        {
            int registered = 0;

            SqlCommand cmd = new SqlCommand("INSERT INTO TCategory (Category_Name, User_Id) VALUES (@Category_Name, @User_Id)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Category_Name", name);
            cmd.Parameters.AddWithValue("@User_Id", userid);
            con.Open();
            registered = cmd.ExecuteNonQuery();
            con.Close();

            return registered;
        }
    }
}