using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EasySplitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EasySplitService : IEasySplitService
    {
        DataLayer dl = new DataLayer();

        public bool AuthenticateUser(string id, string password)
        {
            bool userAuthenticated = false;

            userAuthenticated=dl.login(id, password);

            return userAuthenticated;
        }

        public bool RegisterNewUser(string firstName, string lastName, string email, string password)
        {
            bool userRegistered = false;
            int registered = 0;

            //Capatalize the first letter and the rest of the name to lower
            firstName = firstName.ToLower();
            firstName = (firstName.First().ToString().ToUpper()) + (firstName.Substring(1));
            lastName = lastName.ToLower();
            lastName = (lastName.First().ToString().ToUpper()) + (lastName.Substring(1));

            registered = dl.registerNewUser(firstName, lastName, email, password);

            if(registered==0)
            {
                userRegistered = false;
            }
            else if(registered==1)
            {
                userRegistered = true;
            }

            return userRegistered;
        }

        public bool AddNewCategory(string name, int userid)
        {
            bool categoryAdded = false;
            int rowsAfftected = 0;
            string categoryName;

            //Capatalize the first letter and the rest of the name to lower
            categoryName = name.ToLower();
            categoryName = (categoryName.First().ToString().ToUpper())+(categoryName.Substring(1));

            //Check if the the category already excists for the user

            //Add category to the database
            rowsAfftected = dl.addNewCategory(categoryName, userid);

            if(rowsAfftected==0)
            {
                categoryAdded = false;
            }
            else if (rowsAfftected == 1)
            {
                categoryAdded = true;
            }

            return categoryAdded;
        }
    }
}
