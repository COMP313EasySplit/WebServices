using EasySplitService.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

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
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("registerNewUser_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TUser (Firstname, Lastname, Email, Password) VALUES (@Firstname, @Lastname, @Email, @Password)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Firstname", firstName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                registered = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }

            return registered;
        }

        //Method to create a new event
        public int AddEvent(string name, string budget, string hostid)
        {
            int added = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddEvent_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TEvent (Name, DateCreated, Budget, Status, HostId) VALUES (@Name, GETDATE(), @Budget, @Status, @hostId)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Budget", budget);
                cmd.Parameters.AddWithValue("@Status", "open");
                cmd.Parameters.AddWithValue("@HostId", hostid);
                added = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }

            return added;
        }

        //Method to close an event
        public int CloseEvent(string eventid)
        {
            int closed = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("CloseEvent_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE TEVENT SET Status='closed' WHERE EventId=@EventId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@EventId", eventid);
                closed = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            return closed;
        }

        //Method to update an event
        public int UpdateEvent(string eventid, string name, string budget)
        {
            int updated = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("UpdateEvent_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE TEVENT SET Name=@Name, Budget=@Budget WHERE EventId=@EventId and Status='open'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@EventId", eventid);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Budget", budget);
                updated = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }

            return updated;
        }

        //Method to create a new expense
        public int AddExpense(string eventid, string name, string date, string amount, string place, string originalpayer)
        {
            int added = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddExpense_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TExpense (EventId, Name, DateCreated, Amount, Place, OriginalPayer) VALUES (@EventId, @Name, @DateCreated, @Amount, @Place, @OriginalPayer)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@EventId", eventid);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@DateCreated", date);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Place", place);
                cmd.Parameters.AddWithValue("@OriginalPayer", originalpayer);
                added = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch(Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            return added;
        }


        //Method to update an expense
        public int UpdateExpense(string expenseid, string amount, string originalpayer)
        {
            int updated = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("UpdateExpense_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE TExpense  SET Amount=@Amount, OriginalPayer=@OriginalPayer WHERE ExpenseId=@ExpenseId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@OriginalPayer", originalpayer);
                cmd.Parameters.AddWithValue("@ExpenseId", expenseid);
                updated = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            return updated;
        }


        //Method to delete an expense
        public int DeleteExpense(string expenseid)
        {
            int updated = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("DeleteExpense_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE TExpense WHERE ExpenseId=@ExpenseId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@ExpenseId", expenseid);
                updated = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
            return updated;
        }


        //Method to show all events for a hosts
        public Event[] ShowHostEvents(string hostid)
        {

            SqlDataAdapter dataAdapter;
            DataSet dataSet= new DataSet("Events");
            String sqlCommand = null;
            
            try
            {
                sqlCommand = "Select TEvent.EventId,TEvent.Name,TEvent.DateCreated,Budget,Status ,sum(isnull(amount,0)) as TotalExpense from TEvent left join TExpense on TEvent.EventId=TExpense.EventId where HostId="+hostid+" group by TEvent.EventId,TEvent.Name,TEvent.DateCreated,Budget,Status order by TEvent.DateCreated desc ";

                con.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, con);
                dataAdapter.Fill(dataSet,"Event");

                int size=dataSet.Tables["Event"].Rows.Count;
                Event[] events=new Event[size];
                int count = 0;

                foreach(DataRow dr in dataSet.Tables["Event"].Rows)
                {
                    Event objEvent= new Event();
                    objEvent.EventId=int.Parse(dr["EventId"].ToString());
                    objEvent.Name=dr["Name"].ToString();
                    objEvent.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToString("yyyy-MM-dd");
                    objEvent.Budget=double.Parse(dr["Budget"].ToString());
                    objEvent.Status=dr["Status"].ToString();
                    objEvent.TotalSpend = double.Parse(dr["TotalExpense"].ToString());

                    events[count] = objEvent;
                    count++;
                }

                return events;
            }
            catch (Exception e)
            {
                //Handle exception
                //Log stack trace for exception in a text file
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        //Method to show all participants for an event
        public Participants[] ShowEventParticipants(string eventid)
        {

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet("Participants");
            String sqlCommand = null;

            try
            {
                sqlCommand = "Select EM.UserId, U.Firstname, U.Lastname, U.Email from TEventMembers EM join TUser U on EM.UserId=U.User_Id where EM.EventId=" + eventid;

                con.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, con);
                dataAdapter.Fill(dataSet, "Participant");

                int size = dataSet.Tables["Participant"].Rows.Count;
                Participants[] participants = new Participants[size];
                int count = 0;


                foreach (DataRow dr in dataSet.Tables["Participant"].Rows)
                {
                    Participants objParticipant = new Participants();
                    objParticipant.Userid = int.Parse(dr["UserId"].ToString());
                    objParticipant.Firstname = dr["Firstname"].ToString();
                    objParticipant.Lastname = dr["Lastname"].ToString();
                    objParticipant.Email = dr["Email"].ToString();

                    participants[count] = objParticipant;
                    count++;
                }

                return participants;
            }
            catch (Exception e)
            {
                //Handle exception
                //Log stack trace for exception in a text file
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        //Method to show all expenses for an event
        public Expense[] ShowEventExpense(string eventid)
        {

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet("Expenses");
            String sqlCommand = null;

            try
            {
                sqlCommand = "Select E.ExpenseId, E.Name, E.Amount, E.Place, E.DateCreated, U.User_Id, U.Firstname, U.Lastname, U.Email from TExpense E JOIN TUser U ON E.OriginalPayer=U.User_Id where E.EventId=" + eventid;

                con.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, con);
                dataAdapter.Fill(dataSet, "Expense");

                int size = dataSet.Tables["Expense"].Rows.Count;
                Expense[] expenses = new Expense[size];
                int count = 0;


                foreach (DataRow dr in dataSet.Tables["Expense"].Rows)
                {
                    Expense objExpense = new Expense();
                    objExpense.ExpenseID = int.Parse(dr["ExpenseId"].ToString());
                    objExpense.Name = dr["Name"].ToString();
                    objExpense.Amount = double.Parse(dr["Amount"].ToString());
                    objExpense.Place = dr["Place"].ToString();
                    objExpense.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToString("yyyy-MM-dd");
                    objExpense.OriginalPayer.Userid = int.Parse(dr["User_Id"].ToString());
                    objExpense.OriginalPayer.Firstname = dr["Firstname"].ToString();
                    objExpense.OriginalPayer.Lastname = dr["Lastname"].ToString();
                    objExpense.OriginalPayer.Email = dr["Email"].ToString();
                    expenses[count] = objExpense;
                    count++;
                }

                return expenses;
            }
            catch (Exception e)
            {
                //Handle exception
                //Log stack trace for exception in a text file
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        //Method to show all participants for an host as history
        public Participants[] ShowParticipantHistory(string hostid)
        {

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet("Participants");
            String sqlCommand = null;

            try
            {
                sqlCommand = "Select EM.UserId, U.Firstname, U.Lastname, U.Email from TEventMembers EM join TUser U on EM.UserId=U.User_Id where EM.EventId IN (select eventid from TEvent where hostid=" + hostid + ")";

                con.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, con);
                dataAdapter.Fill(dataSet, "Participant");

                int size = dataSet.Tables["Participant"].Rows.Count;
                Participants[] participants = new Participants[size];
                int count = 0;


                foreach (DataRow dr in dataSet.Tables["Participant"].Rows)
                {
                    Participants objParticipant = new Participants();
                    objParticipant.Userid = int.Parse(dr["UserId"].ToString());
                    objParticipant.Firstname = dr["Firstname"].ToString();
                    objParticipant.Lastname = dr["Lastname"].ToString();
                    objParticipant.Email = dr["Email"].ToString();

                    participants[count] = objParticipant;
                    count++;
                }

                return participants;
            }
            catch (Exception e)
            {
                //Handle exception
                //Log stack trace for exception in a text file
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        //Method to upload image to database
        public void UploadImage(int expenseid, Stream image)
        {
            byte[] fileData = new byte[(int)image.Length];

            image.Read(fileData, 0, (int)image.Length);
            image.Close();

            SqlCommand cmd = new SqlCommand("INSERT INTO TExpenseImage (ExpenseId, Image) VALUES (@ExpenseId, @Image)");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@ExpenseId", expenseid);

            SqlParameter imageParameter = new SqlParameter("@Image", SqlDbType.Image);
            imageParameter.Value = fileData;
            cmd.Parameters.Add(imageParameter);

            cmd.ExecuteNonQuery();
        }
    }
}