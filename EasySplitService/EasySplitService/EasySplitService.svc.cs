using EasySplitService.Entities;
using System;
using System.Collections.Generic;
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


        public bool AuthenticateUser(string id, string password)
        {
            bool userAuthenticated = false;

            userAuthenticated=dl.login(id, password);

            return userAuthenticated;
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
        public bool AddEvent(string name, string budget, string hostid)
        {
            int rowsAfftected = 0;
            bool eventCreated = false;

            //Check for null inputs
            if (name == null || budget == null || hostid == null)
            {
                return eventCreated;
            }
            else
            {
                rowsAfftected = dl.AddEvent(name, budget, hostid);
            }
            
            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                eventCreated = false;
            }
            else if (rowsAfftected == 1)
            {
                eventCreated = true;
            }

            return eventCreated;
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
        public bool AddExpense(string eventid, string name, string date, string amount, string place, string originalpayer)
        {
            int rowsAfftected = 0;
            bool expenseCreated = false;

            //Check for null inputs
            if (eventid==null || name == null || date == null || place == null || originalpayer==null)
            {
                return expenseCreated;
            }
            else
            {
                rowsAfftected = dl.AddExpense(eventid, name, date, amount, place, originalpayer);
            }

            //Check if row was inserted in database
            if (rowsAfftected == 0)
            {
                expenseCreated = false;
            }
            else if (rowsAfftected == 1)
            {
                expenseCreated = true;
            }

            return expenseCreated;
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
    }
}
