﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService
{
    public class Event
    {
        int eventId;
        public int EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }
     

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        string dateCreated;

        public string DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
       

        double budget;
        public double Budget
        {
            get { return budget; }
            set { budget = value; }
        }


        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
       
    }
}