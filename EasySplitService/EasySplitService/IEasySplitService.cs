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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEasySplitService
    {
        //Method to authenticate login
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "login/{id}/{password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AuthenticateUser(string id, string password);

        //Method to register a new user
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "registerNewUser/{firstName}/{lastName}/{email}/{password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool RegisterNewUser(string firstName, string lastName, string email, string password);

        //Method to create a new event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addEvent/{name}/{budget}/{hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddEvent(string name, string budget, string hostid);

        //Method to close an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CloseEvent/{eventid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CloseEvent(string eventid);

        //Method to update an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateEvent/{eventid}/{name}/{budget}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateEvent(string eventid, string name, string budget);

        //Method to create a new expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addExpense/{eventid}/{name}/{date}/{amount}/{place}/{originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddExpense(string eventid, string name, string date, string amount, string place, string originalpayer);

        //Method to update an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateExpense/{expenseid}/{amount}/{originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateExpense(string expenseid, string amount, string originalpayer);

        //Method to delete an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "deleteExpense/{expenseid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool DeleteExpense(string expenseid);

        //Method to show all events for a participant
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "showHostEvents/{hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Event[] ShowHostEvents(string hostid);

        //Method to show all participants for an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getParticipants/{eventid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Participants[] ShowEventParticipants(string eventid);

        //Method to show all expenses for an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getExpense/{eventid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Expense[] ShowEventExpense(string eventid);
    }
}
