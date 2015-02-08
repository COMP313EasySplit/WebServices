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
        [WebGet(UriTemplate = "/login?id={id}&password={password}", ResponseFormat=WebMessageFormat.Json)]
        bool AuthenticateUser(string id,string password);

        //Method to register a new user
        [OperationContract]
        [WebGet(UriTemplate = "/registerNewUser?firstName={firstName}&lastName={lastName}&email={email}&password={password}")]
        bool RegisterNewUser(string firstName, string lastName, string email,string password);


        //This is incomplete. Implement checking of category before adding to see if it exists.
        [OperationContract]
        [WebGet(UriTemplate = "/addCategory?name={name}&userid={userid}")]
        bool AddNewCategory(string name, int userid);

        [OperationContract]
        [WebGet(UriTemplate = "/findFriends?input={input}")]
        void FindFriends(string input);

        //Method to create a new event
        [OperationContract]
        [WebGet(UriTemplate = "/addEvent?name={name}&date={date}&budget={budget}&hostid={hostid}")]
        bool AddEvent(string name, DateTime date, double budget,int hostid);

        //Method to close an event
        [OperationContract]
        [WebGet(UriTemplate = "/closeEvent?eventid={eventid}")]
        bool CloseEvent(int eventid);

        //Method to update an event
        [OperationContract]
        [WebGet(UriTemplate = "/updateEvent?eventid={eventid}&name={name}&budget={budget}")]
        bool UpdateEvent(int eventid,string name, double budget);
    }
}
