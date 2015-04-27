using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace JSONParser
{
    class Program
    {
        public class Tags
        {
            public List<object> name { get; set; }
        }

        public class Event
        {
            public object message { get; set; }
            public object location { get; set; }
            public string time { get; set; }
            public string source { get; set; }
            public string mood { get; set; }
            public Tags tags { get; set; }
            public object rsvp_status { get; set; }
            public string pic_url { get; set; }
        }

        public class RootObject
        {
            public string date { get; set; }
            public string mood { get; set; }
            public int score { get; set; }
            public List<Event> events { get; set; }
        }

        static void Main(string[] args)
        {
            string json = File.ReadAllText(@"C:\Users\Samarth\Desktop\DataBase Project\dbVirtualDiary\dbVirtualDiary\sampleJsonResFinal.json");
            RootObject newTarget = JsonConvert.DeserializeObject<RootObject>(json);
        }
    }
}

