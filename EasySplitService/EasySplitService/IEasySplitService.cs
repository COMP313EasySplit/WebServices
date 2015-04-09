using EasySplitService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
        User AuthenticateUser(string id, string password);

        //Method to register a new user
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "registerNewUser/{firstName}/{lastName}/{email}/{password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool RegisterNewUser(string firstName, string lastName, string email, string password);

        //Method to create a new event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addEvent/{name}/{budget}/{hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Int32 AddEvent(string name, string budget, string hostid);

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
        [WebInvoke(Method = "POST", UriTemplate = "addExpense/{eventid}/{name}/{amount}/{place}/{originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddExpense(string eventid, string name, string amount, string place, string originalpayer);

        //Method to update an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateExpense/{expenseid}/{amount}/{originalpayer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateExpense(string expenseid, string amount, string originalpayer);

        //Method to delete an expense entry
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "deleteExpense/{expenseid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool DeleteExpense(string expenseid);

        //Method to show all events for a host
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

        //Method to show all participants for an host as history
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getParticipantByHost/{hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Participants[] ShowParticipantHistory(string hostid);

        //Method to show all events for a participant
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "showParticipantEvents/{hostid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Event[] ShowParticipantEvents(string hostid);

        //Method to recieve an image and store to database
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "uploadImage/{expenseid}/{image}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UploadImage(string expenseid, string image);

        //Method to add or update participants for an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateEventParticipants/{eventid}/{firstname}/{lastname}/{email}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool addEventParticipants(string eventid, string firstname, string lastname, string email);

        //Method to add or update participants sharing an expense
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateExpenseParticipants/{expenseid}/{userid}/{amount}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool addExpenseParticipants(string expenseid, string userid, string amount);

        //Method to display summary for an event
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "showSummary/{eventid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Summary[] showSummary(string eventid);
    }
}
