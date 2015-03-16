using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService
{
    public class Participants
    {
        private int userid;
        private string firstname, lastname, email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }
 
    }
}