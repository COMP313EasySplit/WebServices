﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySplitService.Entities
{
    public class Expense
    {
        private int expenseID;
        private string name, place, dateCreated;
        private double amount;
        private Participants originalPayer = new Participants();


        public Participants OriginalPayer
        {
            get { return originalPayer; }
            set { originalPayer = value; }
        }

        public string DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

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

        public int ExpenseID
        {
            get { return expenseID; }
            set { expenseID = value; }
        }
    }
}