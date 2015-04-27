using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using dbVirtualDiary.myfirstwebservicerefernce;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;



namespace dbVirtualDiary
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
                Response.Redirect("Login.aspx");

        }

        private RootObject getData(string dateString, string userId)
        {
            WebRequest req = WebRequest.Create(@"http://localhost:8080/DBP/ws/getData?date=" + dateString + "&userId=" + userId);

            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string stringResponse = reader.ReadToEnd();
                    if (stringResponse != String.Empty)
                    {
                        RootObject ro = JsonConvert.DeserializeObject<RootObject>(stringResponse);
                        return ro;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public void Display(RootObject ro)
        {
            if (ro.events.Count > 0)
            {
                //CODE FOR EVENTS
                Events(ro);
            }
        }

        private void Events(RootObject ro)
        {
            ro.events = ro.events.OrderBy(x => x.time).ToList();

            TableRow newRowa = new TableRow();
            TableCell newCella = new TableCell();
            newCella.Height = 30;
            newRowa.Cells.Add(newCella);
            tblDisplay.Rows.Add(newRowa);

            if(ro.mood != null)
            {

                TableRow newRow9 = new TableRow();
                TableCell newCell9 = new TableCell();
                Label lblevents9 = new Label();
                lblevents9.ID = "overallMood";
                lblevents9.Text = "OVERALL MOOD";
                lblevents9.Font.Size = 18;
                lblevents9.ForeColor = System.Drawing.Color.SlateBlue;
                newCell9.Controls.Add(lblevents9);
                newRow9.Cells.Add(newCell9);
                tblDisplay.Rows.Add(newRow9);

                TableRow newRow8 = new TableRow();
                TableCell newCell8 = new TableCell();
                Image imgpic8 = new Image();
                imgpic8.ID = "pic_mood";
                imgpic8.Height = 100;
                imgpic8.Width = 100;
                if (ro.mood == "neutral")
                {
                    imgpic8.ImageUrl = "~/images/neutral-face.png";
                }
                else if (ro.mood == "positive")
                {
                    imgpic8.ImageUrl = "~/images/happy-face.jpg";
                }
                else if (ro.mood == "negative")
                {
                    imgpic8.ImageUrl = "~/images/sad-face.jpg";
                }

                newCell8.Controls.Add(imgpic8);
                newRow8.Cells.Add(newCell8);
                tblDisplay.Rows.Add(newRow8);

                TableRow newRowd = new TableRow();
                TableCell newCelld = new TableCell();
                newCelld.Height = 30;
                newRowd.Cells.Add(newCelld);
                tblDisplay.Rows.Add(newRowd);
            }
            
            TableRow newRow = new TableRow();
            TableCell newCell = new TableCell();
            Label lblevents = new Label();
            lblevents.ID = "events";
            lblevents.Text = "EVENTS";
            lblevents.Font.Size = 18;
            lblevents.ForeColor = System.Drawing.Color.SlateBlue;
            newCell.Controls.Add(lblevents);
            newRow.Cells.Add(newCell);
            tblDisplay.Rows.Add(newRow);

            TableRow newRowb = new TableRow();
            TableCell newCellb = new TableCell();
            newCellb.Height = 20;
            newRowb.Cells.Add(newCellb);
            tblDisplay.Rows.Add(newRowb);

            String latlong = string.Empty;

            for (int i = 0; i < ro.events.Count; i++)
            {

                //For latitude and longitude 
                if(ro.events[i].location != null && ro.events[i].lat != null && ro.events[i].lon != null)
                {
                    latlong += ro.events[i].location + ";" + ro.events[i].lat + ";" + ro.events[i].lon + ";";
                }

                //For Time
                if(ro.events[i].time != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell3 = new TableCell();
                    Label lbltime = new Label();
                    //lbltime.ID = "time" + i;
                    lbltime.Text = ro.events[i].time;
                    newCell3.Controls.Add(lbltime);
                    newRow1.Cells.Add(newCell3);
                    tblDisplay.Rows.Add(newRow1);
                }


                if (ro.events[i].location != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    Label lblvenue = new Label();
                    lblvenue.ID = "venue" + i;
                    lblvenue.Text = "Venue : " + ro.events[i].location;
                    newCell1.Controls.Add(lblvenue);
                    newRow1.Cells.Add(newCell1);

                    TableCell newCell2 = new TableCell();
                    newCell2.Width = 50;
                    newRow1.Cells.Add(newCell2);
                    tblDisplay.Rows.Add(newRow1);

                    //TableCell newCell4 = new TableCell();
                    //Label lbtime = new Label();
                    //lbtime.ID = "time" + i;
                    //lbtime.Text = ro.events[i].time;
                    //newCell4.Controls.Add(lbtime);
                    //newRow1.Cells.Add(newCell4);
                    tblDisplay.Rows.Add(newRow1);
                }

                //For Message
                if (ro.events[i].message != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    Label lblmsg = new Label();
                    lblmsg.ID = "message" + i;
                    lblmsg.Text = "Message : " + ro.events[i].message;
                    newCell1.Controls.Add(lblmsg);
                    newRow1.Cells.Add(newCell1);
                    tblDisplay.Rows.Add(newRow1);
                }

                //For RSVP_Status ---- if there is a facebook event
                if (ro.events[i].rsvp_status != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    Label lblrsvp = new Label();
                    lblrsvp.ID = "rsvp" + i;
                    lblrsvp.Text = "RSVP Status : " + ro.events[i].rsvp_status;
                    newCell1.Controls.Add(lblrsvp);
                    newRow1.Cells.Add(newCell1);
                    tblDisplay.Rows.Add(newRow1);
                }

                //For PhotoURL
                if (ro.events[i].pic_url != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    Image imgpic = new Image();
                    imgpic.ID = "pic" + i;
                    imgpic.ImageUrl = ro.events[i].pic_url;
                    newCell1.Controls.Add(imgpic);
                    newRow1.Cells.Add(newCell1);
                    tblDisplay.Rows.Add(newRow1);
                }

                //For Tags 
                if (ro.events[i].tags.name.Count != 0)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    Label lbltags = new Label();
                    lbltags.ID = "tags" + i;
                    lbltags.Text = "With ";

                    List<object> t = new List<object>();
                    t = ro.events[i].tags.name;
                    var lasttag = t.Last();
                    foreach (var tags in t)
                    {
                        if (tags == lasttag)
                        { lbltags.Text += tags + " "; }
                        else
                        { lbltags.Text += tags + ", "; }
                    }
                    newCell1.Controls.Add(lbltags);
                    newRow1.Cells.Add(newCell1);
                    tblDisplay.Rows.Add(newRow1);
                }

                //For mood
                if (ro.events[i].mood != null)
                {
                    TableRow newRow1 = new TableRow();
                    TableCell newCell1 = new TableCell();
                    //Panel panel = new Panel();
                    Label lblmood = new Label();
                    lblmood.ID = "mood" + i;
                    lblmood.Text = "Mood " + " : " + ro.events[i].mood;
                    //panel.CssClass = "paper";
                    //panel.Controls.Add(lbmood);
                    //newCell1.Controls.Add(panel);
                    
                    newCell1.Controls.Add(lblmood);
                    newRow1.Cells.Add(newCell1);
                    tblDisplay.Rows.Add(newRow1);

                    TableRow newRow2 = new TableRow();
                    TableCell newCell2 = new TableCell();
                    newCell2.Height = 20;
                    newRow2.Cells.Add(newCell2);
                    tblDisplay.Rows.Add(newRow2);
                }
            }

            if (latlong.Length > 0)
            {
                latlong = latlong.Remove(latlong.Length - 1);
                txtmapdata.Text = latlong;
            }
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate1;
            if (DateTime.TryParse(startdate.Text, out selectedDate1))
            {
                string dateString = selectedDate1.ToString("yyyy-MM-dd");
                RootObject ro = getData(dateString, Session["userid"].ToString());
                if (ro != null)
                    Display(ro);
            }
        }
    }
}