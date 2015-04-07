using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService.Entities
{
    public class User
    {
        public int UserId;
        public string FirstName;
        public string LastName;
        public string Email;
        //public string Status;
        //public string DateCreated;
        public User()
        {
            UserId = 0;
            FirstName = "";
            LastName = "";
            Email = "";
        }
    }
}