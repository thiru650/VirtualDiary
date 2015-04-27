using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace dbVirtualDiary
{
    /// <summary>
    /// Summary description for demo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class demo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public String MyFirstWebMethod(String firstName, String lastName)
        {
            //return "How are you " + firstName + " " + lastName + "?";
            return String.Format("How are you {0} {1}?", firstName, lastName);
        }

        [WebMethod]
        public String MySecondWebMethod(String date)
        {
            return String.Format("Date is :  {0} ", date);
        }
    
    }
}
