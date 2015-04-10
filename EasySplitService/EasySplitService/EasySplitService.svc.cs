using EasySplitService.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EasySplitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EasySplitService : IEasySplitService
    {
        DataLayer dl = new DataLayer();


        public User AuthenticateUser(string id, string password)
        {
            User user;
            //bool userAuthenticated = false;

            //userAuthenticated=dl.login(id, password);
            user = dl.login(id, password);
            return user;
        }


        public bool RegisterNewUser(string firstName, string lastName, string email, string password)
        {
            bool userRegistered = false;
            int registered = 0;

            //Capatalize the first letter and the rest of the name to lower
            firstName = firstName.ToLower();
            firstName = (firstName.First().ToString().ToUpper()) + (firstName.Substring(1));
            lastName = lastName.ToLower();
            lastName = (lastName.First().ToString().ToUpper()) + (lastName.Substring(1));

            registered = dl.registerNewUser(firstName, lastName, email, password);

            if(registered==0)
            {
                userRegistered = false;
            }
            else if(registered==1)
            {
                userRegistered = true;
            }

            return userRegistered;
        }

        //Method to create a new event
        public Int32 AddEvent(string name, string budget, string hostid)
        {
            int newEventid = 0;

            //Check for null inputs
            if (name == null || budget == null || hostid == null)
            {
                return -1;
            }
            else
            {
                newEventid = dl.AddEvent(name, budget, hostid);
            }

            return newEventid;
        }


        //Method to close an event
        public bool CloseEvent(string eventid)
        {
            int rowsAfftected = 0;
            bool eventClosed = false;

            //Check for null inputs
            if (eventid.Equals("0"))
            {
                return eventClosed;
            }
            else
            {
                rowsAfftected = dl.CloseEvent(eventid);
            }

            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                eventClosed = false;
            }
            else if (rowsAfftected == 1)
            {
                eventClosed = true;
            }

            return eventClosed;
        }


        //Method to update an event
        public bool UpdateEvent(string eventid, string name, string budget)
        {
            int rowsAfftected = 0;
            bool eventUpdated = false;

            //Check for null inputs
            if (eventid == null || name == null || budget == null)
            {
                return eventUpdated;
            }
            else
            {
                rowsAfftected = dl.UpdateEvent(eventid,name,budget);
            }

            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                eventUpdated = false;
            }
            else if (rowsAfftected == 1)
            {
                eventUpdated = true;
            }

            return eventUpdated;
        }


        //Method to create a new expense
        public Int32 AddExpense(string eventid, string name, string amount, string place, string originalpayer)
        {
            //Check for null inputs
            if (eventid==null || name == null || place == null || originalpayer==null)
            {
                return -1;
            }
            else
            {
                return dl.AddExpense(eventid, name, amount, place, originalpayer);
            }
        }

        //Method to update an expense
        public bool UpdateExpense(string expenseid, string amount, string originalpayer)
        {
            int rowsAfftected = 0;
            bool expenseUpdated = false;

            //Check for null inputs
            if (expenseid == null || amount == null || originalpayer == null)
            {
                return expenseUpdated;
            }
            else
            {
                rowsAfftected = dl.UpdateExpense(expenseid, amount, originalpayer);
            }

            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                expenseUpdated = false;
            }
            else if (rowsAfftected == 1)
            {
                expenseUpdated = true;
            }

            return expenseUpdated;
        }

        //Method to delete an expense
        public bool DeleteExpense(string expenseid)
        {
            int rowsAfftected = 0;
            bool expenseDeleted = false;

            //Check for null inputs
            if (expenseid == null)
            {
                return expenseDeleted;
            }
            else
            {
                rowsAfftected = dl.DeleteExpense(expenseid);
            }

            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                expenseDeleted = false;
            }
            else if (rowsAfftected == 1)
            {
                expenseDeleted = true;
            }

            return expenseDeleted;
        }


        //Method to show all events for a participant
        public Event[] ShowHostEvents(string hostid)
        {
            Event[] events = dl.ShowHostEvents(hostid);

            return events;
        }


        //Method to show all participants for an event
        public Participants[] ShowEventParticipants(string eventid)
        {
            Participants[] participants = dl.ShowEventParticipants(eventid);

            return participants;
        }


        //Method to show all expense for an event
        public Expense[] ShowEventExpense(string eventid)
        {
            Expense[] expenses = dl.ShowEventExpense(eventid);

            return expenses;
        }


        //Method to show all participants for an host as history
        public Participants[] ShowParticipantHistory(string hostid)
        {
            Participants[] participants = dl.ShowParticipantHistory(hostid);

            return participants;
        }


        //Method to show all events for a participant
        public Event[] ShowParticipantEvents(string hostid)
        {
            Event[] events = dl.ShowParticipantEvents(hostid);

            return events;
        }


        //Method to upload an image and save to database
        public string UploadImage(string expenseid, String image)
        {
            string message;

            MemoryStream imageStream = new MemoryStream(Encoding.UTF8.GetBytes(image));

            message = dl.UploadImage(expenseid, imageStream);
            return message;
        }


        //Method to add or update participants for an event
        public bool addEventParticipants(string eventid, string firstname, string lastname, string email)
        {
            bool updated = false;
            int added = 0;

            added = dl.addEventParticipants(eventid, firstname, lastname, email);

            if(added>0)
            {
                updated = true;
            }
            else
            {
                updated = false;
            }

            return updated;
        }


        //Method to add or update participants sharing an expense
        public bool addExpenseParticipants(string expenseid, string userid, string amount)
        {
            bool updated = false;
            int added = 0;

            added = dl.addExpenseParticipants(expenseid, userid, amount);

            if (added > 0)
            {
                updated = true;
            }
            else
            {
                updated = false;
            }

            return updated;
        }


        //Method to display summary for an event
        //public Summary[] showSummary(string eventid)
        public List<String[]> showSummary(string eventid)
        {
            //Summary[] eventSummary = dl.showSummary(eventid);
            //return eventSummary;
            return dl.showSummary(eventid);
        }
    }
}
