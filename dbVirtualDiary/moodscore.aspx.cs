using Newtonsoft.Json;
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
    public partial class moodscore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void btndateClick_Click(object sender, EventArgs e)
        {
            DateTime selectedDate1;
            DateTime selectedDate2;
            string date1String, date2String;
                
            if(DateTime.TryParse(startdate.Text, out selectedDate1) && DateTime.TryParse(enddate.Text, out selectedDate2))
            {
                date1String = selectedDate1.ToString("yyyy-MM-dd");
                date2String = selectedDate2.ToString("yyyy-MM-dd");
                RootObject1 ro = getData(date1String, date2String, Session["userid"].ToString());
                if (ro.scores.Length > 0)
                {
                    String s = String.Empty;                    
                    foreach (var sc in ro.scores)
                    {
                        s += sc.ToString() + ",";
                    }

                    s = s.Substring(0, s.Length - 1);
                    txtdata.Text = s;
                }

                int total = ro.facebook + ro.foursquare + ro.twitter + ro.gcal;
                float fbPercentage = 0, fsqPercentage = 0, twitterPercentage = 0, gcalPercentage = 0;
                if(total > 0)
                {
                    fbPercentage = (float)ro.facebook / (float)total;
                    fsqPercentage = (float)ro.foursquare / (float)total;
                    twitterPercentage = (float)ro.twitter / (float)total;
                    gcalPercentage = (float)ro.gcal / (float)total;
                }

                string pieChartDataValue = "Facebook," + fbPercentage + ",Foursquare," + fsqPercentage + ",Google Calendar," + gcalPercentage + ",Twitter," + twitterPercentage;                
                pieChartData.Text = pieChartDataValue;

                int totalMood = ro.happy + ro.sad + ro.mixed;
                float happyPercentage = 0, sadPercentage = 0, mixedPercentage = 0;
                if (totalMood > 0)
                {
                    happyPercentage = (float)ro.happy / (float)totalMood;
                    sadPercentage = (float)ro.sad / (float)totalMood;
                    mixedPercentage = (float)ro.mixed / (float)totalMood;
                }

                string moodPieChartDataValue = "Happy," + happyPercentage + ",Sad," + sadPercentage + ",Mixed," + mixedPercentage;                
                moodPieChartData.Text = moodPieChartDataValue;
            }
        }

        private RootObject1 getData(string date1String,string date2String, string userId)
        {
            WebRequest req = WebRequest.Create(@"http://localhost:8080/DBP/ws/getMood?startDate=" + date1String+"&endDate="+date2String + "&userId=" + userId);
        
            req.Method = "GET";
            req.Timeout = 300000;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string stringResponse = reader.ReadToEnd();
                    if (stringResponse != String.Empty)
                    {
                        RootObject1 ro = JsonConvert.DeserializeObject<RootObject1>(stringResponse);
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
    }
}