using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dbVirtualDiary
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
            public string lat { get; set; }
            public string lon { get; set; }
        }

        public class RootObject
        {
            public string date { get; set; }
            public string mood { get; set; }
            public float score { get; set; }
            public List<Event> events { get; set; }
        }
    
}