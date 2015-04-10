using EasySplitService.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;

namespace EasySplitService
{
    public class DataLayer
    {
        //Initializing variable for database connection
        static string connetionString = "Data Source=54.191.15.241;Initial Catalog=EasySplit;Persist Security Info=True;User ID=esusercomp313;Password=comp313esuser";
        SqlConnection con = new SqlConnection(connetionString);

        public User login(string id, string password)
        {
            //bool found = false;
            User user = new User();
 
            //SqlCommand command = new SqlCommand("Select * from TUser where Email='@ID' and password='@Pass'" );
            //command.CommandType = CommandType.Text;
            //command.Connection = con;
            //command.Parameters.AddWithValue("@ID", id);
            //command.Parameters.AddWithValue("@Pass", password);

            SqlCommand command = new SqlCommand("Select * from TUser where Email='" + id + "' and password='" + password + "'", con);

            SqlDataReader dataReader;
            con.Open();
            dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    //if (password.Trim() == dataReader.GetValue(4).ToString())
                    //{
                        //found = true;
                        user.UserId = dataReader.GetInt32(0);
                        user.FirstName = dataReader.GetString(1);
                        user.LastName = dataReader.GetString(2);
                        user.Email = dataReader.GetString(3);
                    //}
                }
            }
            con.Close();
            return user;
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
        public Int32 AddEvent(string name, string budget, string hostid)
        {
            int added = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddEvent_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TEvent (Name, DateCreated, Budget, Status, HostId) VALUES (@Name, GETDATE(), @Budget, @Status, @hostId); Select @@IDENTITY;");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Budget", budget);
                cmd.Parameters.AddWithValue("@Status", "open");
                cmd.Parameters.AddWithValue("@HostId", hostid);
                added = Int32.Parse((cmd.ExecuteScalar().ToString()));
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                added = -1;
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
        public Int32 AddExpense(string eventid, string name, string amount, string place, string originalpayer)
        {
            Int32 expenseId = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddExpense_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TExpense (EventId, Name, DateCreated, Amount, Place, OriginalPayer) VALUES (@EventId, @Name, getdate(), @Amount, @Place, @OriginalPayer); Select @@IDENTITY;");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@EventId", eventid);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Place", place);
                cmd.Parameters.AddWithValue("@OriginalPayer", originalpayer);
                expenseId = Int32.Parse(cmd.ExecuteScalar().ToString());
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
            return expenseId;
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
            SqlDataAdapter UserIdDataAdapter;
            DataSet dataSet = new DataSet("Expenses");
            DataSet UserIdDataSet = new DataSet("Users");
            String sqlCommand = null;
            String UserIdSqlCommand = null;
            String EventId=null;
            int usersCount;
            Expense.Share[] shares;
            int userIdCount;

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

                    EventId = dr["ExpenseId"].ToString();

                    UserIdSqlCommand = "Select UserId,Amount from TExpenseShare where ExpenseId=" + EventId;
                    UserIdDataAdapter = new SqlDataAdapter(UserIdSqlCommand, con);
                    UserIdDataAdapter.Fill(UserIdDataSet, "Shares");

                    usersCount = UserIdDataSet.Tables["Shares"].Rows.Count;
                    shares = new Expense.Share[usersCount];
                    userIdCount = 0;

                    foreach (DataRow drUsers in UserIdDataSet.Tables["Shares"].Rows)
                    {
                        shares[userIdCount] = new Expense.Share();
                        shares[userIdCount].UserId = int.Parse(drUsers["UserId"].ToString());
                        shares[userIdCount].Amount = double.Parse(drUsers["Amount"].ToString());
                        userIdCount++;
                    }

                    objExpense.Shares = shares;
                    expenses[count] = objExpense;
                    count++;
                    UserIdDataSet.Clear();
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


        //Method to show all events for a participant
        public Event[] ShowParticipantEvents(string userid)
        {

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet("Events");
            String sqlCommand = null;

            try
            {
                sqlCommand = "Select TEvent.EventId,TEvent.Name,TEvent.DateCreated,Budget,Status, sum(isnull(amount,0)) as TotalExpense from TEvent left join TExpense on TEvent.EventId=TExpense.EventId where TEvent.EventId IN (select TEventMembers.EventId from TEventMembers where UserId="+userid+") AND TEvent.HostId<>"+userid+"group by TEvent.EventId,TEvent.Name,TEvent.DateCreated,Budget,Status order by TEvent.DateCreated desc";

                con.Open();
                dataAdapter = new SqlDataAdapter(sqlCommand, con);
                dataAdapter.Fill(dataSet, "Event");

                int size = dataSet.Tables["Event"].Rows.Count;
                Event[] events = new Event[size];
                int count = 0;

                foreach (DataRow dr in dataSet.Tables["Event"].Rows)
                {
                    Event objEvent = new Event();
                    objEvent.EventId = int.Parse(dr["EventId"].ToString());
                    objEvent.Name = dr["Name"].ToString();
                    objEvent.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToString("yyyy-MM-dd");
                    objEvent.Budget = double.Parse(dr["Budget"].ToString());
                    objEvent.Status = dr["Status"].ToString();
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


        //Method to upload image to database
        public string UploadImage(string expenseid, Stream image)
        {
            

            try
            {

                //Image img = Image.FromFile(@"E:\\image.jpg");
                //MemoryStream ms = new MemoryStream();
                //img.Save(ms, ImageFormat.Jpeg);


                byte[] fileData = new byte[(int)image.Length];

                image.Read(fileData, 0, (int)image.Length);
                image.Close();

               

                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TExpenseImage (ExpenseId, Image) VALUES (@ExpenseId, @Image)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@ExpenseId", 1);

                SqlParameter imageParameter = new SqlParameter("@Image", SqlDbType.Image);
                imageParameter.Value = fileData;
                cmd.Parameters.Add(imageParameter);

                cmd.ExecuteNonQuery();

                return "Image Uploaded";
            }
            catch (Exception e)
            {
                //Handle exception
                //Log stack trace for exception in a text file
                return e.StackTrace;
            }
            finally
            {
                con.Close();
            }
        }

        //Method to add or update participants for an event
        public int addEventParticipants(string eventid, string firstname, string lastname, string email)
        {
            int added = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddEventParticipants_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("SP_AddEventParticipants");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@EventId", eventid);
                cmd.Parameters.AddWithValue("@Firstname", firstname);
                cmd.Parameters.AddWithValue("@Lastname", lastname);
                cmd.Parameters.AddWithValue("@Email", email);
                
                added = cmd.ExecuteNonQuery();
                transaction.Commit();
                added = 1;// participant exists in event, ExecuteNonQuery returns -1, but as long as no exception it is ok.
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


        //Method to add or update participants sharing an expense
        public int addExpenseParticipants(string expenseid, string userid, string amount)
        {
            int added = 0;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction("AddExpParticipants_Transaction");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TExpenseShare (ExpenseId, UserId, Amount) values (@ExpenseId,@UserId,@Amount)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@ExpenseId", expenseid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@Amount", amount);

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


        //Method to display summary for an event
        //public Summary[] showSummary(string eventid)
        //{

        //    SqlDataAdapter dataAdapter;
        //    DataSet dataSet = new DataSet("Events");
        //    String sqlCommand = null;

        //    try
        //    {
        //        sqlCommand = "select sum(amount) as amount, userid from TExpenseShare where expenseid in(select expenseid from TExpense where eventid = " + eventid + ") group by userid";

        //        con.Open();
        //        dataAdapter = new SqlDataAdapter(sqlCommand, con);
        //        dataAdapter.Fill(dataSet, "Summary");

        //        int size = dataSet.Tables["Summary"].Rows.Count;
        //        Summary[] eventSummary = new Summary[size];
        //        int count = 0;

        //        foreach (DataRow dr in dataSet.Tables["Summary"].Rows)
        //        {
        //            Summary objSummary = new Summary();
        //            objSummary.UserId = int.Parse(dr["userid"].ToString());
        //            objSummary.Amount = double.Parse(dr["amount"].ToString());

        //            eventSummary[count] = objSummary;
        //            count++;
        //        }

        //        return eventSummary;
        //    }
        //    catch (Exception e)
        //    {
        //        //Handle exception
        //        //Log stack trace for exception in a text file
        //        return null;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public List<String[]> showSummary(string eventid)
        {
            List<String[]> listSummary = new List<String[]>();

            con.Open();
            DataTable dtSummary = new DataTable();

            try
            {
                DataSet dsSummary = SqlHelper.ExecuteDataset(con,"SP_EventSummary", eventid);
                //dtSummary.Columns.Add((new DataColumn("ExpenseID", Type.GetType("System.Int32"))));
                dtSummary.Columns.Add((new DataColumn("Name", Type.GetType("System.String"))));
                DataTable dtUser = dsSummary.Tables[0];
                foreach (DataRow drUser in dtUser.Rows)
                {
                    DataColumn col = new DataColumn("U" + drUser[0], Type.GetType("System.String"));
                    col.DefaultValue = "0.00";
                    dtSummary.Columns.Add(col);
                }

                DataTable dtExpense = dsSummary.Tables[1];
                DataTable dtExpenseDetail = dsSummary.Tables[2];
                foreach (DataRow drExpense in dtExpense.Rows)
                {
                    DataRow drNew = dtSummary.NewRow();
                    //drNew["ExpenseID"] = int.Parse(drExpense[0].ToString());
                    drNew["Name"] = drExpense[1].ToString();

                    foreach (DataRow dtDetail in dtExpenseDetail.Select("expenseid=" + drExpense[0] + ""))
                    {
                        drNew["U" + dtDetail["userid"]] = dtDetail["amount"];
                    }
                    dtSummary.Rows.Add(drNew);
                }

                DataTable dtBalance = dsSummary.Tables[3];
                DataRow drLast = dtSummary.NewRow();
                //drLast["ExpenseID"] = 0;
                drLast["Name"] = "Balance";
                foreach (DataRow drBalance in dtBalance.Rows)
                {
                    drLast["U" + drBalance["userid"]] = drBalance["amount"];
                }
                dtSummary.Rows.Add(drLast);


                String[] title = new string[dtSummary.Columns.Count];
                //title[0] = "ExpenseID";
                title[0] = "Name";
                for ( int i=0;i<dtUser.Rows.Count;i++)
                {
                    title[i + 1] = dtUser.Rows[i][1].ToString();
                }
                listSummary.Add(title);

                foreach (DataRow dr in dtSummary.Rows)
                {
                    String[] row = new string[dtSummary.Columns.Count];
                    for ( int i=0; i<dtSummary.Columns.Count;i++)
                    {
                        row[i] = dr[i].ToString();
                        if (i > 0)
                            row[i] = double.Parse(dr[i].ToString()).ToString("N2");
                    }
                    listSummary.Add(row);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }

            return listSummary;

        }

    }
}