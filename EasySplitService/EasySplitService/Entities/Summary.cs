using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService.Entities
{
    public class Summary
    {
        int userId;
        double amount;


        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
      

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}