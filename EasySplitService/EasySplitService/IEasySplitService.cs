﻿using System;
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

        //Method to create a new event
        [OperationContract]
        [WebGet(UriTemplate = "/addEvent?name={name}&budget={budget}&hostid={hostid}")]
        bool AddEvent(string name, double budget,int hostid);

        //Method to close an event
        [OperationContract]
        [WebGet(UriTemplate = "/closeEvent?eventid={eventid}")]
        bool CloseEvent(int eventid);

        //Method to update an event
        [OperationContract]
        [WebGet(UriTemplate = "/updateEvent?eventid={eventid}&name={name}&budget={budget}")]
        bool UpdateEvent(int eventid,string name, double budget);

        //Method to create a new expense entry
        [OperationContract]
        [WebGet(UriTemplate = "/addExpense?eventid={eventid}&name={name}&date={date}&amount={amount}&place={place}&originalpayer={originalpayer}")]
        bool AddExpense(int eventid, string name, DateTime date, double amount, string place, int originalpayer);

        //Method to update an expense entry
        [OperationContract]
        [WebGet(UriTemplate = "/updateExpense?expenseid={expenseid}&amount={amount}&originalpayer={originalpayer}")]
        bool UpdateExpense(int expenseid, double amount, int originalpayer);

        //Method to delete an expense entry
        [OperationContract]
        [WebGet(UriTemplate = "/deleteExpense?expenseid={expenseid}")]
        bool DeleteExpense(int expenseid);

        //Method to show all events for a participant
        [OperationContract]
        [WebGet(UriTemplate = "/showParticipantEvents?hostid={hostid}")]
        string ShowParticipantEvents(int hostid);

        //Method to show all events
        [OperationContract]
        [WebGet(UriTemplate = "/showAllEvents")]
        string ShowAllEvents();
    }
}
