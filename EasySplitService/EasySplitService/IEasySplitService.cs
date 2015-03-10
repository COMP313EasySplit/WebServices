using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EasySplitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEasySplitService
    {
        //Method to authenticate login
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/login?id={id}&password={password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AuthenticateUser(string id,string password);

        //Method to register a new user
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/registerNewUser?firstName={firstName}&lastName={lastName}&email={email}&password={password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool RegisterNewUser(string firstName, string lastName, string email,string password);

        //Method to create a new event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/addEvent?name={name}&budget={budget}&hostid={hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddEvent(string name, double budget,int hostid);

        //Method to close an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/closeEvent?eventid={eventid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CloseEvent(int eventid);

        //Method to update an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/updateEvent?eventid={eventid}&name={name}&budget={budget}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateEvent(int eventid,string name, double budget);

        //Method to create a new expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/addExpense?eventid={eventid}&name={name}&date={date}&amount={amount}&place={place}&originalpayer={originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddExpense(int eventid, string name, DateTime date, double amount, string place, int originalpayer);

        //Method to update an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/updateExpense?expenseid={expenseid}&amount={amount}&originalpayer={originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateExpense(int expenseid, double amount, int originalpayer);

        //Method to delete an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/deleteExpense?expenseid={expenseid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool DeleteExpense(int expenseid);

        //Method to show all events for a participant
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/showHostEvents?hostid={hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Event[] ShowHostEvents(int hostid);

        //Method to show all events
        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate = "/showAllEvents", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string ShowAllEvents();
    }
}
