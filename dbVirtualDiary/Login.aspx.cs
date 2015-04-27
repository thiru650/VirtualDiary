using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbVirtualDiary
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if(userNameTextBox.Text != String.Empty && passwordTextBox.Text != String.Empty)
            {
                Authenticate(userNameTextBox.Text);
            }
        }

        private void Authenticate(string userName)
        {
            //Get method

            WebRequest req = WebRequest.Create(@"http://localhost:8080/DBP/ws/loginService?userName=" + userName);

            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string userId = reader.ReadToEnd();
                    if(userId != String.Empty)
                    {
                        Session["userid"] = userId;
                        Response.Redirect("main.aspx");
                    }
                    else
                    {
                        ErrorLabel.Text = "No such user exists";                        
                    }
                }
            }
            else
            {
                ErrorLabel.Text = "Error calling web service. Status: " + resp.StatusCode;
            }
        }
    }
}