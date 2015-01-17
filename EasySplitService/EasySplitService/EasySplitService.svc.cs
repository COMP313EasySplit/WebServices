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

        public bool RegisterNewUser(string name, string email, string password)
        {
            bool userRegistered = false;
            int registered = 0;

            registered = dl.registerNewUser(name, email, password);

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
    }
}
