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

        //Method to create a new event
        public int AddEvent(string name, DateTime date, double budget,int hostid)
        {
            int added = 0;

            SqlCommand cmd = new SqlCommand("INSERT INTO TEvent (Name, DateCreated, Budget, Status, HostId) VALUES (@Name, @DateCreated, @Budget, @Status, @hostId)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@DateCreated", date);
            cmd.Parameters.AddWithValue("@Budget", budget);
            cmd.Parameters.AddWithValue("@Status", "open");
            cmd.Parameters.AddWithValue("@HostId", hostid);
            con.Open();
            added = cmd.ExecuteNonQuery();
            con.Close();

            return added;
        }

        //Method to close an event
        public int CloseEvent(int eventid)
        {
            int closed = 0;

            SqlCommand cmd = new SqlCommand("UPDATE TEVENT SET Status='closed' WHERE EventId=@EventId");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@EventId", eventid);
            con.Open();
            closed = cmd.ExecuteNonQuery();
            con.Close();

            return closed;
        }

        //Method to update an event
        public int UpdateEvent(int eventid, string name, double budget)
        {
            int updated = 0;

            SqlCommand cmd = new SqlCommand("UPDATE TEVENT SET Name=@Name, Budget=@Budget WHERE EventId=@EventId and Status='open'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@EventId", eventid);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Budget", budget);
            con.Open();
            updated = cmd.ExecuteNonQuery();
            con.Close();

            return updated;
        }

        //Method to create a new expense
        public int AddExpense(int eventid, string name, DateTime date, double amount, string place, int originalpayer)
        {
            int added = 0;

            SqlCommand cmd = new SqlCommand("INSERT INTO TExpense (EventId, Name, DateCreated, Amount, Place, OriginalPayer) VALUES (@EventId, @Name, @DateCreated, @Amount, @Place, @OriginalPayer)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@EventId", eventid);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@DateCreated", date);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Place", place);
            cmd.Parameters.AddWithValue("@OriginalPayer", originalpayer);
            con.Open();
            added = cmd.ExecuteNonQuery();
            con.Close();

            return added;
        }
    }
}