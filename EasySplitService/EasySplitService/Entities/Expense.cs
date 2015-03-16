using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService.Entities
{
    public class Expense
    {
        private int expenseID, payerID;
        private string name, place;
        private double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Place
        {
            get { return place; }
            set { place = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int PayerID
        {
            get { return payerID; }
            set { payerID = value; }
        }

        public int ExpenseID
        {
            get { return expenseID; }
            set { expenseID = value; }
        }
    }
}