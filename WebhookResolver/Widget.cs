using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookResolver
{
    public class Widget
    {
        public string id { get; set; }
        public string name { get; set; }
        public object instant_invite { get; set; }
        public Channel[] channels { get; set; }
        public Member[] members { get; set; }
        public int presence_count { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
    }

    public class Member
    {
        public string id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public object avatar { get; set; }
        public string status { get; set; }
        public string avatar_url { get; set; }
    }
}
