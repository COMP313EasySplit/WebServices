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

        [OperationContract]
        [WebGet(UriTemplate = "/login?id={id}&password={password}")]
        //[WebGet(UriTemplate = "/login/{id}/{password}")]
        bool AuthenticateUser(string id,string password);

        [OperationContract]
        [WebGet(UriTemplate = "/registerNewUser?firstName={firstName}&lastName={lastName}&email={email}&password={password}")]
        bool RegisterNewUser(string firstName, string lastName, string email,string password);


        //This is incomplete. Implement checking of category before adding to see if it exists.
        [OperationContract]
        [WebGet(UriTemplate = "/addCategory?name={name}&userid={userid}")]
        bool AddNewCategory(string name, int userid);

        //[OperationContract]
        //[WebGet(UriTemplate = "/addCategory?name={name}&userid={userid}")]
        //bool AddNewCategory(string name, int userid);
    }
}
